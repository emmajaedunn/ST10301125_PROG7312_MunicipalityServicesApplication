using MunicipalityApplicatiion.Forms;
using MunicipalityApplicatiion.Repositories;
using System;
using System.Windows.Forms;

namespace MunicipalityApplicatiion.UI
{
    public partial class MainMenu : Form
    {
        private readonly ServiceRequestRepository _repo = new();

        public MainMenu()
        {
            InitializeComponent();
        }

        private void button1_Click(object? s, EventArgs e)
        {
            var reportForm = new ReportIssue(_repo);
            reportForm.Show();
            Hide();
            reportForm.FormClosed += (_, __) => Show();
        }

        private void buttonViewReports_Click(object? sender, EventArgs e)
        {
            var viewForm = new ViewReports(_repo);
            viewForm.Show();
            Hide();
            viewForm.FormClosed += (_, __) => Show();
        }


        private void button2_Click(object? sender, EventArgs e)
        {
            var eventsForm = new LocalEvents();
            eventsForm.Show();
            Hide();
            eventsForm.FormClosed += (_, __) => Show();
        }

        private void button3_Click(object? sender, EventArgs e)
        {
            var statusForm = new ServiceRequestForm(_repo);
            statusForm.Show();
            Hide();
            statusForm.FormClosed += (_, __) => Show();
        }


        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
