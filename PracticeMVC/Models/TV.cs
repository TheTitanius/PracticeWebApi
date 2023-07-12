using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace PracticeMVC.Models
{
    public class TV : INotifyPropertyChanged
    {
        [Column("TVId")]
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private uint price;
        public uint Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged(nameof(Price));
            }
        }

        private uint tvInStock = 0;
        public uint TvInStock
        {
            get { return tvInStock; }
            set
            {
                tvInStock = value;
                OnPropertyChanged(nameof(TvInStock));
            }
        }

        private uint soldNumber = 0;
        public uint SoldNumber
        {
            get { return soldNumber; }
            set
            {
                soldNumber = value;
                OnPropertyChanged(nameof(SoldNumber));
            }
        }

        private uint deliveredNumber = 0;
        public uint DeliveredNumber
        {
            get { return deliveredNumber; }
            set
            {
                deliveredNumber = value;
                OnPropertyChanged(nameof(DeliveredNumber));
            }
        }

        private TVType tvType;
        public TVType TVType
        {
            get { return tvType; }
            set
            {
                tvType = value;
                OnPropertyChanged(nameof(TVType));
            }
        }

        private Manufacturer manufacturer;
        public Manufacturer Manufacturer
        {
            get { return manufacturer; }
            set
            {
                manufacturer = value;
                OnPropertyChanged(nameof(Manufacturer));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
