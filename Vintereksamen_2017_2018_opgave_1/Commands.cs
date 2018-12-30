using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Vintereksamen_2017_2018_opgave_1
{
    public class Commands : INotifyPropertyChanged
    {
        DataAccessLayer _dal = new DataAccessLayer();

        private List<ProductDTO> _products = new List<ProductDTO>();
        public List<ProductDTO> Products
        {
            get { return _products; }
            set
            {
                _products = value;
                NotifyPropertyChanged("Products");
            }
        }
        
        public Commands()
        {
            LoadProducts();
        }

        public async void LoadProducts()
        {
            List<ProductDTO> _temp = await _dal.GetAllProducts();
            Products = _temp;
        }

       
        ICommand _addProductCommand;
        public ICommand AddProductCommand
        {
            get { return _addProductCommand ?? (_addProductCommand = new RelayCommand<object>(AddProduct)); }
        }

        private async void AddProduct(object parameter)
        {
            var values = (object[])parameter;

            string productNumber = values[0].ToString();
            string productName = values[1].ToString();
            string productPrice = values[2].ToString();

            ProductDTO _product = new ProductDTO(productNumber, productName, productPrice);

            bool status = await _dal.Add_Product(_product);

            if (status)
            {
                MessageBox.Show("Product is added to the catalogue.");
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Product could not be added to the catalogue.");
            }
        }
        
        ICommand _updateProductCommand;
        public ICommand UpdateProductCommand
        {
            get { return _updateProductCommand ?? (_updateProductCommand = new RelayCommand<object>(UpdateProduct)); }
        }

        private async void UpdateProduct(object parameter)
        {
            var values = (object[])parameter;

            string productNumber = values[0].ToString();
            string productName = values[1].ToString();
            string productPrice = values[2].ToString();

            ProductDTO _product = new ProductDTO(productNumber, productName, productPrice);

            bool status = await _dal.UpdateProduct(_product);

            if (status)
            {
                MessageBox.Show($"Product with product number {productNumber} is updated in the catalogue.");
                LoadProducts();
            }
            else
            {
                MessageBox.Show($"Product with product number {productNumber} could not be updated in the catalogue.\nBe sure to enter correct product number.");
            }
        }


        ICommand _deleteProductCommand;
        public ICommand DeleteProductCommand
        {
            get { return _deleteProductCommand ?? (_deleteProductCommand = new RelayCommand<object>(DeleteProduct)); }
        }

        private async void DeleteProduct(object parameter)
        {
            var values = (ProductDTO)parameter;

            bool status = await _dal.DeleteProduct(values.ProductNumber);

            if (status)
            {
                MessageBox.Show("Product is removed from the catalogue.");
                LoadProducts();
            }
            else
            {
                MessageBox.Show("Product could not be removed from the catalogue.");
            }
        }

        
        ICommand _createOrderBtnCommand;
        public ICommand CreateOrderBtnCommand
        {
            get { return _createOrderBtnCommand ?? (_createOrderBtnCommand = new RelayCommand(CreateOrderBtn)); }
        }

        private void CreateOrderBtn()
        {
            SalesWindow _s = new SalesWindow();
            _s.ShowDialog();
        }

        // ------ SALESWINDOW ------//

        private ObservableCollection<ProductForOrder> _orderItems = new ObservableCollection<ProductForOrder>();
        public ObservableCollection<ProductForOrder> OrderItems
        {
            get { return _orderItems; }
            set
            {
                _orderItems = value;
                NotifyPropertyChanged("OrderItems");
                NotifyPropertyChanged();
            }
        }

        private int _itemCount;
        public int ItemCount
        {
            get { return _itemCount; }
            set
            {
                _itemCount = (int)value;
                NotifyPropertyChanged("ItemCount");
            }
        }

        ICommand _addProductToOrderCommand;
        public ICommand AddProductToOrderCommand
        {
            get { return _addProductToOrderCommand ?? (_addProductToOrderCommand = new RelayCommand<object>(AddProductToOrder)); }
        }

        private async void AddProductToOrder(object parameter)
        {
            var value = (ProductDTO)parameter;

            var item = new ProductForOrder(ItemCount, value.Name, value.Price);

            _orderItems.Add(item);
            OrderItems = _orderItems;

            TotalPrice += item.TotalPrice;
        }

        private int _totalPrice;
        public int TotalPrice
        {
            get { return _totalPrice; }
            set
            {
                _totalPrice = value;
                NotifyPropertyChanged("TotalPrice");
            }
            
        }

        #region INotifyPropertyChanged implementation

        public new event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
