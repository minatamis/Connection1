using Connection1.Class;
using Connection1.Connection;
using Connection1.Entities;
using Connection1.Model;
using Connection1.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Connection1
{
    public partial class Menu : Form
    {
        private readonly IButtonService _buttonService;
        private readonly ILabelService _labelService;
        private readonly IImageService _imageService;
        private readonly IMenuCategoryService _menuCategoryService;
        private ButtonManagerCreation _buttonManagerCreation;
        private ButtonSize _buttonSize;
        private Label categName;
        private Label tagLine;
        private ButtonConfig _buttonConfig;
        private int _categY;
        private int mainSize;

        public Menu(ButtonSize buttonSize, 
            IButtonService buttonService, 
            ILabelService labelService, 
            IImageService imageService, 
            IMenuCategoryService menuCategoryService)
        {
            InitializeComponent();
            _buttonSize = buttonSize;
            _buttonService = buttonService;
            _labelService = labelService;
            _imageService = imageService;
            _menuCategoryService = menuCategoryService;

            _buttonManagerCreation = new ButtonManagerCreation(buttonService, buttonSize);
            AddOrderList.Click += AddOrderList_Click;
        }

        
        private void Menu_Load(object sender, EventArgs e)
        {
            CreateUIElements();
        }
        private void CreateUIElements()
        {
            InitializeButtonConfig();
            SetMainSize();

            foreach (var item in _menuCategoryService.GetPagedMenuCategories())
            {
                ConfigureButton(item);
                var button = _buttonManagerCreation.CreateButton(_buttonConfig);
                button.Click += Button_Click;
                this.Controls.Add(button);
            }

            _categY = _buttonSize.y;
        }
        private void SetMainSize()
        {
            mainSize = this.Width - this.PricePanel.Width - this.MenuPanel.Width - 45;
            _buttonManagerCreation.Initialize(mainSize);
        }
        private void InitializeButtonConfig()
        {
            _buttonConfig = new ButtonConfig
            {
                Size = new Size(_buttonSize.sizeW, _buttonSize.sizeH)
            };
        }
        private void ConfigureButton(MenuCategory category)
        {
            _buttonConfig.Text = category.CategName.ToUpper();
            _buttonConfig.ImagePath = _imageService.GetImagePath(category.CategImagePath);
            _buttonConfig.TagLine = category.TagLine == null ? "" : category.TagLine.ToUpper();
            _buttonConfig.Id = category.CategId;
        }
        private void Button_Click(object sender, EventArgs e)
        {
            priceTag.Text = "Price : ";
            _buttonManagerCreation.ClearButtons(this.Controls);
            this.Controls.Remove(categName);
            this.Controls.Remove(tagLine);

            Button button = (Button)sender;
            DisplayCategoryDetails(button);
            LoadProductButtons(_buttonManagerCreation.GetDetailsFromButton(button).Id);
        }
        private void DisplayCategoryDetails(Button clickedButton)
        {
            _buttonManagerCreation.ChangeBackColorButton(Color.FromArgb(186, 1, 1), clickedButton);

            categName = _labelService.CreateLabel(
                clickedButton.Text,
                36,
                FontStyle.Bold,
                new Point(_buttonSize.x - 5, _categY + _buttonSize.sizeH + 25),
                Color.FromArgb(251, 189, 13));

            this.Controls.Add(categName);

            tagLine = _labelService.CreateLabel(
                _buttonManagerCreation.GetDetailsFromButton(clickedButton).TagLine,
                12,
                FontStyle.Regular,
                new Point(_buttonSize.x, categName.Location.Y + categName.Height - 10),
                Color.Black);

            this.Controls.Add(tagLine);

            tagLine.BringToFront();
        }
        private void LoadProductButtons(int categoryDetails)
        {
            int x = categName.Location.Y + categName.Height + 25;
            //_buttonSize.x = _buttonSize.x;
            _buttonManagerCreation.ResetPoitionForNewRow(x);
            SetMainSize();

            foreach (var product in _menuCategoryService.GetProductList(categoryDetails))
            {
                _buttonConfig.Text = product.ProductName.ToUpper();
                _buttonConfig.Id = product.ProductId;
                _buttonConfig.price = product.Price;
                var button = _buttonManagerCreation.CreateButton(_buttonConfig, true);
                button.Click += ProductButton_Click;
                this.Controls.Add(button);
            }

        }

        private void ProductButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            _buttonManagerCreation.ClearButtons2();
            _buttonManagerCreation.ChangeBackColorButton(Color.FromArgb(251, 189, 13), button);
            priceTag.Text = $"Price : {_buttonManagerCreation.GetDetailsFromButton(button).Price:F2}";

        }

        private void AddOrderList_Click(object sender, EventArgs e)
        {
            Panel lastPanel = OrderPanel.Controls.OfType<Panel>().LastOrDefault();
            int margin = 10;

            Panel panel = new Panel
            {
                Size = new Size(OrderPanel.Width - 3 * margin, 150 - margin),
                BorderStyle = BorderStyle.FixedSingle,
                Location = lastPanel == null
                    ? new Point(margin - 5, margin)
                    : new Point(margin - 5, lastPanel.Bottom + margin)
            };

            Label productNameLabel = new Label
            {
                Text = "Product Name",
                Location = new Point(10, 10),
                Size = new Size(200, 20)
            };
            panel.Controls.Add(productNameLabel);

            OrderPanel.Controls.Add(panel);
        }

        #region Table
        //public void CreateTable()
        //{
        //    Orderlist.Columns.Add(new DataGridViewButtonColumn
        //    {
        //        Name = "Delete",
        //        HeaderText = "Delete",
        //        Text = "Delete",
        //        UseColumnTextForButtonValue = true
        //    });

        //    Orderlist.Columns.Add(new DataGridViewTextBoxColumn
        //    {
        //        Name = "Name",
        //        HeaderText = "Name"
        //    });

        //    Orderlist.Columns.Add(new DataGridViewButtonColumn
        //    {
        //        Name = "QuantityMinus",
        //        HeaderText = "Quantity Minus",
        //        Text = "-",
        //        UseColumnTextForButtonValue = true
        //    });

        //    Orderlist.Columns.Add(new DataGridViewTextBoxColumn
        //    {
        //        Name = "Quantity",
        //        HeaderText = "Quantity"
        //    });

        //    Orderlist.Columns.Add(new DataGridViewButtonColumn
        //    {
        //        Name = "QuantityPlus",
        //        HeaderText = "Quantity Plus",
        //        Text = "+",
        //        UseColumnTextForButtonValue = true
        //    });

        //    Orderlist.Columns.Add(new DataGridViewTextBoxColumn
        //    {
        //        Name = "Price",
        //        HeaderText = "Price"
        //    });

        //    Orderlist.Rows.Add("Sample Name", 10, 20, 30, 40.50m);
        //}

        #endregion

    }
}

