using MunicipalityApplicatiion.Models;
using MunicipalityApplicatiion.Repositories;

namespace MunicipalityApplicatiion.UI
{
    public sealed class ViewReports : Form
    {
        private readonly ServiceRequestRepository _repo;

        // UI
        private readonly Label lblTitle = new();
        private readonly TextBox txtSearchId = new();
        private readonly Button btnFind = new();
        private readonly ListView lv = new();
        private readonly RichTextBox rtbDetails = new();
        private Button btnBack;

        public ViewReports(ServiceRequestRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));

            // --- Window styling to match your app ---
            Text = "View Submitted Reports";
            MinimumSize = new Size(900, 700);
            Size = new Size(1000, 700);
            BackColor = Color.OldLace;
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // --- Header ---
            lblTitle.Text = "Submitted Reports";
            lblTitle.Font = new Font("Verdana", 20, FontStyle.Bold);
            lblTitle.AutoSize = true;
            lblTitle.Location = new Point(24, 18);

            // --- Search bar ---
            txtSearchId.PlaceholderText = "Paste Report ID (GUID)…";
            txtSearchId.Font = new Font("Verdana", 9);
            txtSearchId.Location = new Point(24, 62);
            txtSearchId.Width = 380;

            btnFind.Text = "Find";
            btnFind.Font = new Font("Verdana", 9, FontStyle.Bold);
            btnFind.Location = new Point(412, 60);
            btnFind.Width = 80;
            btnFind.Click += (_, __) => FindById();

            // Back Button
            btnBack = new Button
            {
                Text = "⬅︎ Back to Main Menu",
                Width = 160,
                Height = 35,
                BackColor = Color.White,
                UseVisualStyleBackColor = true,
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Standard,
                Location = new Point(800, 20),
                Font = new Font("Verdana", 9, FontStyle.Bold),
                Padding = new Padding(3)
            };

            btnBack.Click += (s, e) => Close();

            // --- List (left) ---
            lv.Location = new Point(24, 100);
            lv.Size = new Size(480, 500);
            lv.View = View.Details;
            lv.FullRowSelect = true;
            lv.HideSelection = false;
            lv.MultiSelect = false;
            lv.Columns.Add("Ref (ID)", 230);
            lv.Columns.Add("Title", 170);
            lv.Columns.Add("Status", 80);
            lv.SelectedIndexChanged += (_, __) => ShowSelected();

            // --- Details (right) ---
            rtbDetails.Location = new Point(520, 100);
            rtbDetails.Size = new Size(440, 500);
            rtbDetails.ReadOnly = true;
            rtbDetails.BorderStyle = BorderStyle.FixedSingle;
            rtbDetails.Font = new Font("Verdana", 9);


            Controls.AddRange(new Control[] { lblTitle, txtSearchId, btnFind, lv, rtbDetails, btnBack });

            // Load data when shown
            Shown += (_, __) => Reload();
            Activated += (_, __) => Reload(); // refresh when returning to this window
        }

        private void Reload()
        {
            var items = _repo.All()
                             .OrderByDescending(r => r.CreatedAt)
                             .Select(r => new ListViewItem(new[] { r.RequestId, r.Title, r.Status.ToString() }) { Tag = r })
                             .ToArray();

            lv.BeginUpdate();
            lv.Items.Clear();
            lv.Items.AddRange(items);
            lv.EndUpdate();

            rtbDetails.Clear();
            if (lv.Items.Count > 0) { lv.Items[0].Selected = true; }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide the ReportIssue form

            // Create a new Main Menu instance
            MainMenu mainMenu = new MainMenu();
            mainMenu.Show(); // show the main menu
        }

        private void ShowSelected()
        {
            if (lv.SelectedItems.Count == 0) { rtbDetails.Clear(); return; }

            var req = (ServiceRequest)lv.SelectedItems[0].Tag;
            rtbDetails.Clear();
            rtbDetails.AppendText($"Reference ID: {req.RequestId}\n");
            rtbDetails.AppendText($"Title       : {req.Title}\n");
            rtbDetails.AppendText($"Status      : {req.Status}\n");
            rtbDetails.AppendText($"Priority    : P{req.Priority}\n");
            rtbDetails.AppendText($"Location    : {req.LocationNode}\n");
            rtbDetails.AppendText($"Created     : {req.CreatedAt:g}\n");
            rtbDetails.AppendText($"Updated     : {req.UpdatedAt:g}\n\n");
            rtbDetails.AppendText("Description:\n");
            rtbDetails.AppendText(req.Description ?? "");
        }

        private void FindById()
        {
            var id = txtSearchId.Text?.Trim();
            if (string.IsNullOrWhiteSpace(id))
            {
                MessageBox.Show("Please enter a Report ID.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Use your repo’s fast lookups
            if (_repo.TryGet(id, out var req))
            {
                // select the row
                foreach (ListViewItem it in lv.Items)
                {
                    if (((ServiceRequest)it.Tag).RequestId == req.RequestId)
                    {
                        it.Selected = true;
                        it.Focused = true;
                        it.EnsureVisible();
                        ShowSelected();
                        return;
                    }
                }
            }
            MessageBox.Show("No report found for that ID.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
