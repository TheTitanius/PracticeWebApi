﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace PracticeMVC.Models
{
    public class Client : INotifyPropertyChanged
    {
        [Column("ClientId")]
        public int Id { get; set; }
        private string fullName;
        public string FullName
        {
            get { return fullName; }
            set
            {
                fullName = value;
                OnPropertyChanged(nameof(FullName));
            }
        }

        private string telephone;
        public string Telephone
        {
            get { return telephone; }
            set
            {
                telephone = value;
                OnPropertyChanged(nameof(Telephone));
            }
        }

        private long bankAccount;
        public long BankAccount
        {
            get { return bankAccount; }
            set
            {
                bankAccount = value;
                OnPropertyChanged(nameof(BankAccount));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
