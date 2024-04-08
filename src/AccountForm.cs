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
    }
}