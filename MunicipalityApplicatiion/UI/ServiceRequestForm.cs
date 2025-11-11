using MunicipalityApplicatiion.Repositories;
using MunicipalityApplicatiion.UI;
using MunicipalityApplicatiion.Models;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace MunicipalityApplicatiion.Forms
{
    public partial class ServiceRequestForm : Form
    {
        private readonly ServiceRequestRepository _index;
        private readonly BindingList<ServiceRequest> _binding = new();

        public ServiceRequestForm(ServiceRequestRepository index)
        {
            _index = index ?? throw new ArgumentNullException(nameof(index));
            InitializeComponent();

            // Bind grid once
            if (grid.DataSource == null) grid.DataSource = _binding;

            // Wire only if the controls exist on your form
            if (btnFind != null) btnFind.Click += btnFind_Click;
            if (cboStatus != null) cboStatus.SelectedIndexChanged += (_, __) => RefreshGrid();

            // Subscribe to repository change events
            _index.Changed += (_, __) => RefreshGrid();

            // Show all by default
            if (cboStatus != null) cboStatus.SelectedIndex = -1;

            ApplyTheme();
            RefreshGrid();
        }

        private void ApplyTheme()
        {
            BackColor = Color.OldLace;
            Font = new Font("Verdana", 9F, FontStyle.Regular);

            // Grid look
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = Color.White;
            grid.BorderStyle = BorderStyle.None;

            var alt = new DataGridViewCellStyle { BackColor = Color.FromArgb(248, 245, 240) };
            grid.AlternatingRowsDefaultCellStyle = alt;

            var header = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(230, 230, 230),
                Font = new Font("Verdana", 9F, FontStyle.Bold)
            };
            grid.ColumnHeadersDefaultCellStyle = header;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            RefreshGrid();
        }

        private void btnTopPriority_Click(object? sender, EventArgs e)
        {
            var top = _index.MostUrgent(5);

            _binding.Clear();
            foreach (var r in top)
                _binding.Add(r);

            // Optional: show analytics or message
            lstInfo.Items.Clear();
            lstInfo.Items.Add($"Showing Top {top.Count()} Most Urgent Requests");

            RefreshInsights();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            RefreshGrid();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide the ReportIssue form

            // Create a new Main Menu instance
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show(); // show the main menu
        }

        private void RefreshGrid()
        {
            _binding.Clear();

            // Pull all
            var all = _index.All();

            // Optional filter by status if combo is present
            if (cboStatus?.SelectedItem is string s &&
                Enum.TryParse<RequestStatus>(s, out var chosen))
            {
                all = all.Where(x => x.Status == chosen);
            }

            foreach (var r in all.OrderBy(x => x.Priority).ThenBy(x => x.CreatedAt))
                _binding.Add(r);

            RefreshInsights();
        }

        private void RefreshInsights()
        {
            lstInfo.Items.Clear();

            // ===== 1️⃣ Total Requests =====
            var all = _index.All().ToList();
            lstInfo.Items.Add($"Total Requests: {all.Count}");

            if (all.Count > 0)
            {
                // ===== 2️⃣ Oldest / Newest Request (AVL Tree by CreatedAt) =====
                var ordered = _index.InOrderByCreatedDate().ToList();
                var oldest = ordered.First();
                var newest = ordered.Last();

                lstInfo.Items.Add($"Oldest Request: {oldest.Title} ({oldest.CreatedAt:g})");
                lstInfo.Items.Add($"Newest Request: {newest.Title} ({newest.CreatedAt:g})");

                // ===== 3️⃣ Requests per Status (using LINQ on _all) =====
                var statusGroups = all
                    .GroupBy(r => r.Status)
                    .OrderBy(g => g.Key)
                    .ToList();

                lstInfo.Items.Add("");
                lstInfo.Items.Add("Requests by Status:");
                foreach (var g in statusGroups)
                    lstInfo.Items.Add($"  • {g.Key}: {g.Count()} request(s)");

                // ===== 4️⃣ Top Urgent Requests (Priority Queue) =====
                var topUrgent = _index.MostUrgent(5).ToList();
                if (topUrgent.Count > 0)
                {
                    lstInfo.Items.Add("");
                    lstInfo.Items.Add("Top Urgent Requests:");
                    foreach (var u in topUrgent)
                        lstInfo.Items.Add($"  • {u.Title} (Priority {u.Priority})");
                }

                // ===== 5️⃣ Connected Locations (Graph BFS) =====
                var bfsLocations = _index.AreaBfs().ToList();
                if (bfsLocations.Any())
                {
                    lstInfo.Items.Add("");
                    lstInfo.Items.Add("Connected Locations (BFS):");
                    lstInfo.Items.Add("  " + string.Join(" → ", bfsLocations));
                }

                // ===== 6️⃣ Minimum Spanning Tree Edges (Graph) =====
                var mstEdges = _index.AreaMst().ToList();
                if (mstEdges.Any())
                {
                    lstInfo.Items.Add("");
                    lstInfo.Items.Add("Area MST Edges:");
                    foreach (var e in mstEdges)
                        lstInfo.Items.Add($"  {e.U} ↔ {e.V} (w={e.W})");
                }

                // ===== 7️⃣ Titles in Alphabetical Order (RBT) =====
                lstInfo.Items.Add("");
                lstInfo.Items.Add("Requests by Title (A-Z):");
                var titles = all.Select(r => r.Title ?? string.Empty).OrderBy(t => t).ToList();
                foreach (var t in titles)
                    lstInfo.Items.Add($"  • {t}");
            }
            else
            {
                lstInfo.Items.Add("No requests available.");
            }
        }

        private void TrackById()
        {
            var id = txtSearchId.Text?.Trim();
            if (string.IsNullOrEmpty(id)) return;

            if (_index.TryGet(id, out var req))
                MessageBox.Show($"Found:\n{req}", "Track Request");
            else
                MessageBox.Show("Not found.");
        }

        private void btnFind_Click(object? sender, EventArgs e) => TrackById();

        // Only keep this if you actually have a Back button on the form
       
    }
}