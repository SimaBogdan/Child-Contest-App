using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Forms;
using src.Model;
using src.Service;

namespace src
{
    public partial class AccountForm : Form
    {
        private static IService _service;
        private static Organizator _currentOrganizator;
        
        public AccountForm(IService service, Organizator currentOrganizator)
        {
            InitializeComponent();
            _service = service;
            _currentOrganizator = currentOrganizator;
        }
        
        private void LoadDataGridViews(object sender, EventArgs e)
        {
            dataGridViewEvents.AutoGenerateColumns = true;
            dataGridViewEvents.DataSource = _service.getAllProba();
            dataGridViewEvents.Columns.Remove("idProba");
            
            dataGridViewEvents.Columns.Add("varsta_min", "Varsta Minima");
            dataGridViewEvents.Columns.Add("varsta_max", "Varsta Maxima");
            dataGridViewEvents.Columns.Add("tip_proba", "Tipul Probei");
            dataGridViewEvents.Columns.Add("noParticipants", "No. Participants");
            
            foreach (DataGridViewRow row in dataGridViewEvents.Rows)
            {
                if (row.DataBoundItem is Proba proba)
                {
                    row.Cells["varsta_min"].Value = proba.VarstaMin;
                    row.Cells["varsta_max"].Value = proba.VarstaMax;
                    row.Cells["tip_proba"].Value = proba.TipProba;
                    var noParticipants = CountParticipants(proba);
                    row.Cells["noParticipants"].Value = noParticipants;
                }
            }
            
            dataGridViewParticipants.AutoGenerateColumns = true;
            dataGridViewParticipants.DataSource = _service.getAllParticipant();
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
        
        private static int CountParticipants(Proba proba)
        {
            var concursuri = _service.findByProba(proba.IdProba);
            var participantsCountMap = new Dictionary<int, int>();

            foreach (var concurs in concursuri)
            {
                var probaId = concurs.IdProba;
                if (participantsCountMap.ContainsKey(probaId))
                {
                    participantsCountMap[probaId]++;
                }
                else
                {
                    participantsCountMap[probaId] = 1;
                }
            }

            return participantsCountMap.ContainsKey(proba.IdProba) ? participantsCountMap[proba.IdProba] : 0;
        }
        
        private static int CountRegistrations(Participant participant)
        {
            var registrations = _service.findByParticipant(participant.id);
            return registrations.Count;
        }
        
        private void HandleSearch(object sender, EventArgs e)
        {
            try
            {
                var selectedRow = dataGridViewEvents.SelectedRows[0];
                var varsta_min = Convert.ToInt32(selectedRow.Cells["varsta_min"].Value);
                var varsta_max = Convert.ToInt32(selectedRow.Cells["varsta_max"].Value);
                var tip_proba = Convert.ToString((selectedRow.Cells["tip_proba"].Value));
                var proba = _service.findByVarstaSiTip(varsta_min, varsta_max, tip_proba);
                
                var searchForm = new SearchForm(_service, proba);
                searchForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while showing search dialog: " + ex.Message);
            }
        }
        
        private void HandleRegister(object sender, EventArgs e)
        {
            try
            {
                if (dataGridViewParticipants.SelectedRows.Count == 0)
                {
                    var registerForm = new RegisterForm(_service, _currentOrganizator, null);
                    registerForm.ShowDialog();   
                }
                else
                {
                    var selectedRow = dataGridViewParticipants.SelectedRows[0];
                    var firstName = selectedRow.Cells["nume"].Value;
                    var lastName = selectedRow.Cells["prenume"].Value;
                    var age = selectedRow.Cells["varsta"].Value;
                    var participant = _service.findOneByNumeSiVarsta(firstName.ToString(), lastName.ToString(), Convert.ToInt32(age));    
                    
                    if (_service.findByParticipant(participant.id).Count == 2)
                        MessageAlert.ShowErrorMessage(null, "This participant is already registered in the maximum number of events (2)!");
                    else
                    {
                        var registerForm = new RegisterForm(_service, _currentOrganizator, participant);
                        registerForm.ShowDialog();   
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occurred while showing register dialog: " + ex.Message);
            }
        }


    }
}