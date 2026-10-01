using System.ComponentModel;
using System.Text;

namespace Bai3_TechMartProductManager
{
    public partial class Form1 : Form
    {
        private readonly BindingList<Product> _products = new BindingList<Product>();
        private string? _imagePath;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // cboCategory: Điện thoại, Laptop, Phụ kiện
            List<Category> categories = new List<Category>
            {
                new Category { CategoryId = 1, CategoryName = "Điện thoại" },
                new Category { CategoryId = 2, CategoryName = "Laptop" },
                new Category { CategoryId = 3, CategoryName = "Phụ kiện" }
            };
            cboCategory.DataSource = categories;
            cboCategory.DisplayMember = "CategoryName";
            cboCategory.ValueMember = "CategoryId";

            // BindingSource + BindingList<T> làm trung gian Data Binding
            bindingSource1.DataSource = _products;
            dgvProducts.DataSource = bindingSource1;

            UpdateStatus();
        }

        // Kiểm tra không để trống Tên SP, Đơn giá > 0, Số lượng >= 0
        private bool ValidateInput(out decimal unitPrice, out int quantity)
        {
            errorProvider1.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(txtProductName.Text))
            {
                errorProvider1.SetError(txtProductName, "Tên SP không được để trống!");
                valid = false;
            }

            if (!decimal.TryParse(txtUnitPrice.Text, out unitPrice) || unitPrice <= 0)
            {
                errorProvider1.SetError(txtUnitPrice, "Đơn giá phải lớn hơn 0!");
                valid = false;
            }

            if (!int.TryParse(txtQuantity.Text, out quantity) || quantity < 0)
            {
                errorProvider1.SetError(txtQuantity, "Số lượng phải lớn hơn hoặc bằng 0!");
                valid = false;
            }

            return valid;
        }

        private void FillProduct(Product p, decimal unitPrice, int quantity)
        {
            Category category = (Category)cboCategory.SelectedItem!;
            p.ProductId = txtProductId.Text.Trim();
            p.ProductName = txtProductName.Text.Trim();
            p.CategoryId = category.CategoryId;
            p.CategoryName = category.CategoryName;
            p.UnitPrice = unitPrice;
            p.Quantity = quantity;
            p.ImagePath = _imagePath;
        }

        // Thêm mới: Kiểm tra Validation → Thêm vào danh sách → Cập nhật DataGridView
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidateInput(out decimal unitPrice, out int quantity))
                return;

            Product p = new Product();
            FillProduct(p, unitPrice, quantity);
            _products.Add(p);
            ApplySearch();
        }

        // Cập nhật: Sửa thông tin sản phẩm đang chọn trên Grid
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is not Product p)
                return;
            if (!ValidateInput(out decimal unitPrice, out int quantity))
                return;

            FillProduct(p, unitPrice, quantity);
            ApplySearch();
        }

        // Xóa: Bật Dialog xác nhận Yes/No trước khi xóa
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (bindingSource1.Current is not Product p)
                return;

            DialogResult result = MessageBox.Show("Bạn có chắc muốn xóa sản phẩm này?", "Xác nhận xóa",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                _products.Remove(p);
                ApplySearch();
            }
        }

        // Tìm kiếm: Live search theo tên sản phẩm
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            ApplySearch();
        }

        private void ApplySearch()
        {
            string keyword = txtSearch.Text.Trim();
            if (keyword == "")
            {
                bindingSource1.DataSource = _products;
            }
            else
            {
                bindingSource1.DataSource = new BindingList<Product>(_products
                    .Where(p => p.ProductName.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                    .ToList());
            }
            bindingSource1.ResetBindings(false);
            UpdateStatus();
        }

        // Click chọn 1 dòng trên DataGridView → Dữ liệu tự nạp ngược lên các ô nhập liệu
        private void dgvProducts_CellClick(object? sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || bindingSource1.Current is not Product p)
                return;

            txtProductId.Text = p.ProductId;
            txtProductName.Text = p.ProductName;
            cboCategory.SelectedValue = p.CategoryId;
            txtUnitPrice.Text = p.UnitPrice.ToString();
            txtQuantity.Text = p.Quantity.ToString();
            _imagePath = p.ImagePath;
            picAvatar.ImageLocation = p.ImagePath;
            errorProvider1.Clear();
        }

        // Chọn ảnh bằng OpenFileDialog
        private void btnChooseImage_Click(object sender, EventArgs e)
        {
            using OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp;*.gif";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                _imagePath = ofd.FileName;
                picAvatar.ImageLocation = _imagePath;
            }
        }

        // Xuất CSV dùng SaveFileDialog
        private void btnExportCsv_Click(object? sender, EventArgs e)
        {
            using SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "CSV Files|*.csv";
            sfd.FileName = "products.csv";
            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Mã SP,Tên SP,Danh Mục,Đơn Giá,Số Lượng");
            foreach (Product p in _products)
            {
                sb.AppendLine($"{p.ProductId},{p.ProductName},{p.CategoryName},{p.UnitPrice},{p.Quantity}");
            }
            File.WriteAllText(sfd.FileName, sb.ToString(), new UTF8Encoding(true));
        }

        private void exitToolStripMenuItem_Click(object? sender, EventArgs e)
        {
            Close();
        }

        // StatusStrip: "Tổng số sản phẩm: X"
        private void UpdateStatus()
        {
            lblStatus.Text = $"Tổng số sản phẩm: {_products.Count}";
        }
    }
}
