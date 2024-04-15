using System;
using System.Collections;
using System.Windows.Forms;
using src.Model;
using src.Service;

namespace src
{
    public partial class RegisterForm : Form
    {
        private static IService _service;
        private static Organizator _currentOrganizator;
        private static Participant _currentParticipant;
        
        public RegisterForm(IService service, Organizator currentOrganizator, Participant currentParticipant)
        {
            InitializeComponent();
            _service = service;
            _currentOrganizator = currentOrganizator;
            _currentParticipant = currentParticipant;
        }
        
         public void LoadData(object sender, EventArgs e)
        {
            var ages = new ArrayList();
            
            for (var i = 6; i <= 15; i++)
            {
                ages.Add(i);
            }
            
            comboBoxAge.Items.AddRange(ages.ToArray());

            if (_currentParticipant != null)
            {
                textBoxFirstName.Text = _currentParticipant.Nume;
                textBoxLastName.Text = _currentParticipant.Prenume;
                comboBoxAge.SelectedIndex = _currentParticipant.Varsta - 6;
                comboBoxAge.Enabled = false;

                var registeredEvents = new ArrayList();
                foreach (var concurs in _service.findByParticipant(_currentParticipant.Id))
                {
                    registeredEvents.Add(_service.findOnProba(concurs.IdProba));
                }

                var probe = new ArrayList();
                
                if (_currentParticipant.Varsta >= 6 && _currentParticipant.Varsta <= 8)
                {
                    probe.Add("desen");
                }
                else if (_currentParticipant.Varsta >= 9 && _currentParticipant.Varsta <= 11)
                {
                    probe.Add("cautarea unei comori");
                }
                else if (_currentParticipant.Varsta >= 12 && _currentParticipant.Varsta <= 15)
                {
                    probe.Add("poezie");
                }
                
                foreach (var registeredEvent in registeredEvents)
                {
                    probe.Remove(registeredEvent);
                }

                Console.WriteLine();
                comboBoxEvent.Items.AddRange(probe.ToArray());
                comboBoxEvent.SelectedIndex = 0;
            }
            else
            {
                comboBoxAge.SelectedIndex = 0;
                
                comboBoxAge.SelectedIndexChanged += (sender1, e1) =>
                {
                    var varsta = (int)comboBoxAge.SelectedItem;
                    
                    var probe = new ArrayList();
                    
                    if (varsta >= 6 && varsta <= 8)
                    {
                        probe.Add("desen");
                    }
                    else if (varsta >= 9 && varsta <= 11)
                    {
                        probe.Add("cautarea unei comori");
                    }
                    else if (varsta >= 12 && varsta <= 15)
                    {
                        probe.Add("poezie");
                    }
                    
                    comboBoxEvent.Items.Clear();
                    comboBoxEvent.Items.AddRange(probe.ToArray());
                    comboBoxEvent.SelectedIndex = 0; 
                };
                
                comboBoxAge.Select();
            }
        }
         
          private void HandleRegister(object sender, EventArgs e)
        {
            if (_currentParticipant == null)
            {
                var random = new Random();
                var id = random.Next(100000);
                var nume = textBoxFirstName.Text;
                var prenume = textBoxLastName.Text;
                var varsta = Convert.ToInt32(comboBoxAge.SelectedItem);

                var participant = _service.findOneByNumeSiVarsta(nume, prenume, varsta);

                if (participant == null)
                {
                    participant = new Participant(id, nume, prenume, varsta);

                    _currentParticipant = participant;

                    _service.addParticipant(participant);
                    MessageAlert.ShowMessage(null, MessageBoxIcon.Information, "Register Confirmation", "Participant registered successfully!");
                }
                else
                {
                    MessageAlert.ShowErrorMessage(null, "This participant already exists and you have to select it from the previous page!");
                }

                var tip_proba = comboBoxEvent.SelectedItem;
                var varsta_min = 6;
                var varsta_max = 8;
                
                if (varsta >= 9 && varsta <= 11)
                {
                    varsta_min = 9;
                    varsta_max = 11;
                }
                else if (varsta >= 12 && varsta <= 15)
                {
                    varsta_min = 12;
                    varsta_max = 15;
                }

                var proba = _service.findByVarstaSiTip(varsta_min, varsta_max, tip_proba.ToString());
                
                var idConcurs = random.Next(100000);
                var concurs = new Concurs(idConcurs, participant.id, proba.IdProba, _currentOrganizator.id);
                _service.addConcurs(concurs);
                Console.WriteLine("Registration successfully added!");
            }
            else
            {
                var tip_proba = comboBoxEvent.SelectedItem;
                var varsta = Convert.ToInt32(comboBoxAge.SelectedItem);
                var varsta_min = 6;
                var varsta_max = 8;
                
                if (varsta >= 9 && varsta <= 11)
                {
                    varsta_min = 9;
                    varsta_max = 11;
                }
                else if (varsta >= 12 && varsta <= 15)
                {
                    varsta_min = 12;
                    varsta_max = 15;
                }

                var proba = _service.findByVarstaSiTip(varsta_min, varsta_max, tip_proba.ToString());

                var random = new Random();
                var idConcurs = random.Next(100000);
                var concurs = new Concurs(idConcurs, _currentParticipant.Id, proba.IdProba, _currentOrganizator.id);
                _service.addConcurs(concurs);
                Console.WriteLine("Registration successfully added!");
            }
        }

    }
}