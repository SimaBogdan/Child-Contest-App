using System;
using System.Collections;
using System.Windows.Forms;
using src.Model;
using src.Service;

namespace src
{
    public partial class SearchForm : Form
    {
        private static IService _service;
        private static Proba _currentProba;
        
        public SearchForm(IService service, Proba currentProba)
        {
            InitializeComponent();
            _service = service;
            _currentProba = currentProba;
        }
        
        private void LoadDataGridView(object sender, EventArgs e)
        {
            dataGridViewParticipants.AutoGenerateColumns = true;

            var participants = new ArrayList();
            foreach (var concurs in _service.findByProba(_currentProba.IdProba))
            {
                participants.Add(_service.findOne(concurs.IdParticipant));
            }
            
            dataGridViewParticipants.DataSource = participants;
            dataGridViewParticipants.Columns.Remove("id");
            
            dataGridViewParticipants.Columns.Add("noRegistrations", "No. Registrations");
            
            foreach (DataGridViewRow row in dataGridViewParticipants.Rows)
            {
                if (row.DataBoundItem is Participant participant)
                {
                    var noRegistrations = CountRegistrations(participant);
                    row.Cells["noRegistrations"].Value = noRegistrations;
                }
            }
        }
        
        private static int CountRegistrations(Participant participant)
        {
            var concurs = _service.findByParticipant(participant.id);
            return concurs.Count;
        }
    }
}