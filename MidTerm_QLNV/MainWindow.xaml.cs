using Microsoft.Data.SqlClient;
using MidTerm_QLNV.NhanVien;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MidTerm_QLNV
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Đặt giá trị mặc định cho ngày vào làm là ngày hiện tại
            dtpNgayVaoLam.SelectedDate = DateTime.Now;

            using (SqlConnection connection = new SqlConnection(@"server=OYEEWIN\LECHIHAO;database=CSDL_QLNV;uid=sa;pwd=sa;Encrypt=False"))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM ThongtinNhanVien";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThongtinNhanVien employee = new ThongtinNhanVien
                        {
                            MaNV = reader["MaNV"].ToString(),
                            TenNV = reader["TenNV"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            NgayVaoLam = Convert.ToDateTime(reader["NgayVaoLam"]),
                            DienThoai = reader["DienThoai"].ToString(),
                            DoanhSo = Convert.ToDecimal(reader["DoanhSo"]),
                            PhuCap = Convert.ToDecimal(reader["PhuCap"]),
                        };
                        lstNhanVien.Items.Add(employee);
                        
                    }
                }
                
            }
            // Gọi hàm để kiểm tra và tô nền vàng cho các nhân viên có thâm niên trên 5 năm
            HighlightEmployeesWith5YearsOrMore();

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn đóng chương trình?", "Xác nhận đóng", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Hủy đóng nếu người dùng chọn "No"
            }
        }

        private void txtMaNV_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void txtUsername_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox inputBox = (TextBox)sender;
            if (inputBox.Text == "Vui lòng nhập tài khoản" ||
                inputBox.Text == "Vui lòng nhập tên" ||
                inputBox.Text == "Vui lòng nhập số điện thoại" ||
                inputBox.Text == "Vui lòng nhập doanh số" ||
                inputBox.Text == "Vui lòng nhập PC nhiên liệu")
            {
                inputBox.Text = "";
                inputBox.Foreground = Brushes.Black;
            }
        }

        private void txtUsername_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox inputBox = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(inputBox.Text))
            {
                if (inputBox.Name == "txtMaNV")
                {
                    inputBox.Text = "Vui lòng nhập tài khoản";
                }
                else if (inputBox.Name == "txtTenNV")
                {
                    inputBox.Text = "Vui lòng nhập tên";
                }
                else if (inputBox.Name == "txtDienThoai")
                {
                    inputBox.Text = "Vui lòng nhập số điện thoại";
                }
                else if (inputBox.Name == "txtDoanhSo")
                {
                    inputBox.Text = "Vui lòng nhập doanh số";
                }
                else if (inputBox.Name == "txtPhuCap")
                {
                    inputBox.Text = "Vui lòng nhập PC nhiên liệu";
                }
                inputBox.Foreground = Brushes.Gray;
            }
        }
        private async void HighlightEmployeesWith5YearsOrMore()
        {
            await Task.Delay(100);
            foreach (ThongtinNhanVien employee in lstNhanVien.Items)
            {
                // Tính thâm niên
                int yearsOfWork = DateTime.Now.Year - employee.NgayVaoLam.Year;
                if (yearsOfWork >= 5)
                {
                    // Tô nền vàng cho dòng dữ liệu
                    ListViewItem listViewItem = (ListViewItem)lstNhanVien.ItemContainerGenerator.ContainerFromItem(employee);
                    if (listViewItem != null)
                    {
                        listViewItem.Background = Brushes.Yellow;
                    }
                }
            }
        }
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (radBanHang.IsChecked == true)
            {
                lblDoanhSo.Visibility = Visibility.Visible;
                txtDoanhSo.Visibility = Visibility.Visible;
            }

            if (radGiaoNhan.IsChecked == true)
            {
                lblPhuCap.Visibility = Visibility.Visible;
                txtPhuCap.Visibility = Visibility.Visible;
            }

        }
        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            if (radBanHang.IsChecked == false)
            {
                lblDoanhSo.Visibility = Visibility.Collapsed;
                txtDoanhSo.Visibility = Visibility.Collapsed;
            }

            if (radGiaoNhan.IsChecked == false)
            {
                lblPhuCap.Visibility = Visibility.Collapsed;
                txtPhuCap.Visibility = Visibility.Collapsed;
            }


        }
        private void SetDefaultText(TextBox inputBox)
        {
            if (string.IsNullOrWhiteSpace(inputBox.Text))
            {
                if (inputBox.Name == "txtMaNV")
                {
                    inputBox.Text = "Vui lòng nhập tài khoản";
                }
                else if (inputBox.Name == "txtTenNV")
                {
                    inputBox.Text = "Vui lòng nhập tên";
                }
                else if (inputBox.Name == "txtDienThoai")
                {
                    inputBox.Text = "Vui lòng nhập số điện thoại";
                }
                else if (inputBox.Name == "txtDoanhSo")
                {
                    inputBox.Text = "Vui lòng nhập doanh số";
                }
                else if (inputBox.Name == "txtPhuCap")
                {
                    inputBox.Text = "Vui lòng nhập PC nhiên liệu";
                }
                inputBox.Foreground = Brushes.Gray;
            }
        }

        private void ResetInputBox(TextBox inputBox)
        {
            if (inputBox.Text == "Vui lòng nhập tài khoản" ||
                inputBox.Text == "Vui lòng nhập tên" ||
                inputBox.Text == "Vui lòng nhập số điện thoại" ||
                inputBox.Text == "Vui lòng nhập doanh số" ||
                inputBox.Text == "Vui lòng nhập PC nhiên liệu")
            {
                inputBox.Text = "";
                inputBox.Foreground = Brushes.Black;
            }
        }
        private void ClearAndSetDefaults()
        {
            // Xóa dữ liệu TextBox
            txtMaNV.Clear();
            txtTenNV.Clear();
            txtDoanhSo.Clear();
            txtDienThoai.Clear();
            txtPhuCap.Clear();

            // Đặt lại mặc định cho các TextBox
            SetDefaultText(txtMaNV);
            SetDefaultText(txtTenNV);
            SetDefaultText(txtDoanhSo);
            SetDefaultText(txtDienThoai);
            SetDefaultText(txtPhuCap);

            // Đặt giá trị mặc định cho ngày vào làm là ngày hiện tại
            dtpNgayVaoLam.SelectedDate = DateTime.Now;

            // Đặt lại các ô RadioButton
            radNu.IsChecked = false;
            radGiaoNhan.IsChecked = false;

            radNam.IsChecked = true;
            radBanHang.IsChecked = true;

            // Đặt con trỏ vào TextBox mã nhân viên
            txtMaNV.Focus();
        }


        private void Button_Click(object sender, RoutedEventArgs e) //Nút thêm
        {
            ClearAndSetDefaults();
        }

        private bool ValidateInput()
        {
            // Kiểm tra mã nhân viên và họ tên không được để trống
            if (string.IsNullOrWhiteSpace(txtMaNV.Text) || txtMaNV.Text == "Vui lòng nhập mã nhân viên")
            {
                MessageBox.Show("Mã nhân viên đang bị bỏ trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtTenNV.Text) || txtTenNV.Text == "Vui lòng nhập tên")
            {
                MessageBox.Show("Họ tên đang bị bỏ trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Kiểm tra doanh số hoặc PC nhiên liệu không được để trống (tùy theo loại NV)
            if (string.IsNullOrWhiteSpace(txtDoanhSo.Text) || txtDoanhSo.Text == "Vui lòng nhập doanh số")
            {
                MessageBox.Show("Doanh số đang bị bỏ trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            else if (!string.IsNullOrWhiteSpace(txtDoanhSo.Text) & radBanHang.IsChecked == true)
            {
                ;
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtPhuCap.Text) || txtPhuCap.Text == "Vui lòng nhập PC nhiên liệu")
                {
                    MessageBox.Show("PC nhiên liệu đang bị bỏ trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            // Kiểm tra SĐT không được để trống
            if (string.IsNullOrWhiteSpace(txtDienThoai.Text) || txtDienThoai.Text == "Vui lòng nhập số điện thoại")
            {
                MessageBox.Show("Số điện thoại đang bị bỏ trống!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Kiểm tra số điện thoại chỉ bao gồm số
            long phoneNumber;
            if (!long.TryParse(txtDienThoai.Text, out phoneNumber)) // chuyển đổi một chuỗi thành một giá trị số nguyên
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            // Kiểm tra ngày vào làm không lớn hơn ngày hiện tại
            if (dtpNgayVaoLam.SelectedDate > DateTime.Today)
            {
                MessageBox.Show("Ngày vào làm không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true; // Tất cả thông tin hợp lệ
        }


        private async void btnLuu_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateInput())
            {
                // Lấy giới tính từ RadioButton
                string gioiTinh = radNam.IsChecked == true ? "Nam" : "Nữ";

                // Lấy loại nhân viên từ RadioButton
                string nhanVien = radBanHang.IsChecked == true ? "Bán hàng" : "Giao nhận";

                // Lấy ngày vào làm từ DatePicker
                DateTime ngayVaoLam = dtpNgayVaoLam.SelectedDate ?? DateTime.Now; // Nếu không chọn ngày, lấy ngày hiện tại

                // Tạo đối tượng Nhân viên mới từ thông tin
                ThongtinNhanVien newEmployee = new ThongtinNhanVien
                {
                    MaNV = txtMaNV.Text,
                    TenNV = txtTenNV.Text,
                    GioiTinh = gioiTinh,
                    NgayVaoLam = ngayVaoLam,
                    NhanVien = nhanVien,
                    DienThoai = txtDienThoai.Text,
                };

                if (decimal.TryParse(txtDoanhSo.Text, out decimal doanhSo))
                {
                    newEmployee.DoanhSo = doanhSo;
                }
                else
                {
                    newEmployee.DoanhSo = 0; // Hoặc giá trị mặc định nếu không thể chuyển đổi
                }

                if (decimal.TryParse(txtPhuCap.Text, out decimal phuCap))
                {
                    newEmployee.PhuCap = phuCap;
                }
                else
                {
                    newEmployee.PhuCap = 0; // Hoặc giá trị mặc định nếu không thể chuyển đổi
                }
                // Thêm newEmployee vào đầu danh sách Items
                lstNhanVien.Items.Insert(0, newEmployee);

                // Đợi một lúc để đảm bảo giao diện hiển thị hoàn tất
                await Task.Delay(100);

                // Kiểm tra và tô nền vàng cho các nhân viên có thâm niên trên 5 năm
                HighlightEmployeesWith5YearsOrMore();

                //Chọn dòng mới thêm trên listview
                lstNhanVien.SelectedItem = newEmployee;

                // Lưu dữ liệu vào cơ sở dữ liệu
                string strConn = @"server=OYEEWIN\LECHIHAO;database=CSDL_QLNV;uid=sa;pwd=sa;Encrypt=False";
                SqlConnection connection = new SqlConnection(strConn);

                connection.Open();
                string insertQuery = "INSERT INTO ThongtinNhanVien (MaNV, TenNV, GioiTinh, NgayVaoLam, NhanVien, DienThoai, DoanhSo, PhuCap) " +
                                     "VALUES (@MaNV, @TenNV, @GioiTinh, @NgayVaoLam, @NhanVien, @DienThoai, @DoanhSo, @PhuCap)";
                using (SqlCommand command = new SqlCommand(insertQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaNV", newEmployee.MaNV);
                    command.Parameters.AddWithValue("@TenNV", newEmployee.TenNV);
                    command.Parameters.AddWithValue("@GioiTinh", newEmployee.GioiTinh);
                    command.Parameters.AddWithValue("@NgayVaoLam", newEmployee.NgayVaoLam);
                    command.Parameters.AddWithValue("@NhanVien", newEmployee.NhanVien);
                    command.Parameters.AddWithValue("@DienThoai", newEmployee.DienThoai);
                    command.Parameters.AddWithValue("@DoanhSo", newEmployee.DoanhSo);
                    command.Parameters.AddWithValue("@PhuCap", newEmployee.PhuCap);
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        private void DisplayEmployeeData(ThongtinNhanVien selectedEmployee)
        {
            using (SqlConnection connection = new SqlConnection(@"server=OYEEWIN\LECHIHAO;database=CSDL_QLNV;uid=sa;pwd=sa;Encrypt=False"))
            {
                connection.Open();
                string selectQuery = "SELECT * FROM ThongtinNhanVien WHERE MaNV = @MaNV";
                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                {
                    command.Parameters.AddWithValue("@MaNV", selectedEmployee.MaNV);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {

                            ThongtinNhanVien newEmployee = new ThongtinNhanVien
                            {
                                MaNV = reader["MaNV"].ToString(),
                                TenNV = reader["TenNV"].ToString(),
                                GioiTinh = reader["GioiTinh"].ToString(),
                                NgayVaoLam = Convert.ToDateTime(reader["NgayVaoLam"]),
                                DienThoai = reader["DienThoai"].ToString(),
                                DoanhSo = Convert.ToDecimal(reader["DoanhSo"]),
                                PhuCap = Convert.ToDecimal(reader["PhuCap"]),
                                NhanVien = reader["NhanVien"].ToString(),
                            };

                            txtMaNV.Text = newEmployee.MaNV.ToString();
                            txtTenNV.Text = newEmployee.TenNV.ToString();
                            txtDienThoai.Text = newEmployee.DienThoai.ToString();
                            dtpNgayVaoLam.SelectedDate = newEmployee.NgayVaoLam;

                            // Chọn giới tính tương ứng
                            radNam.IsChecked = newEmployee.GioiTinh == "Nam";
                            radNu.IsChecked = newEmployee.GioiTinh == "Nữ";

                            // Chọn loại nhân viên tương ứng
                            if (newEmployee.NhanVien == "Bán hàng")
                            {
                                radBanHang.IsChecked = newEmployee.NhanVien == "Bán hàng";
                                txtDoanhSo.Text = newEmployee.DoanhSo.ToString();

                            }
                            else if (newEmployee.NhanVien == "Giao nhận")
                            {
                                txtPhuCap.Text = newEmployee.PhuCap.ToString();
                                radGiaoNhan.IsChecked = newEmployee.NhanVien == "Giao nhận";
                            }
                        }

                    }
                }
            }

        }
        private async void lstNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstNhanVien.SelectedItem != null)
            {
                ThongtinNhanVien selectedEmployee = (ThongtinNhanVien)lstNhanVien.SelectedItem;
                DisplayEmployeeData(selectedEmployee);

            }
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {

            if (lstNhanVien.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    int selectedIndex = lstNhanVien.SelectedIndex;
                    lstNhanVien.Items.RemoveAt(selectedIndex);

                    // Chọn dòng liền kề sau khi xóa
                    if (selectedIndex < lstNhanVien.Items.Count)
                    {
                        lstNhanVien.SelectedIndex = selectedIndex;
                    }
                    else if (lstNhanVien.Items.Count > 0)
                    {
                        lstNhanVien.SelectedIndex = selectedIndex - 1;
                    }
                }
            }
            if (lstNhanVien.Items.Count == 0)
            {
                ClearAndSetDefaults();
            }
        }

        private bool isUpdating = false;

        private async void btnSua_Click(object sender, RoutedEventArgs e)
        {
            if (lstNhanVien.SelectedItem != null)
            {
                ThongtinNhanVien selectedEmployee = (ThongtinNhanVien)lstNhanVien.SelectedItem;

                string strConn = @"server=OYEEWIN\LECHIHAO;database=CSDL_QLNV;uid=sa;pwd=sa;Encrypt=False";

                // Cập nhật dữ liệu trong database
                string updateQuery1 = "UPDATE ThongtinNhanVien " +
                    "SET DoanhSo = @DoanhSo " +
                    "WHERE MaNV = @MaNV";
                string updateQuery2 = "UPDATE ThongtinNhanVien " +
                    "SET PhuCap = @PhuCap " +
                    "WHERE MaNV = @MaNV";

                using (SqlConnection connection = new SqlConnection(strConn))
                {
                    connection.Open();

                    if (selectedEmployee.NhanVien == "Bán hàng")
                    {
                        if (int.TryParse(txtDoanhSo.Text, out int doanhso))
                        {
                            selectedEmployee.DoanhSo = doanhso;
                            Console.WriteLine(doanhso);
                        }
                        else
                        {
                            MessageBox.Show("Doanh số không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        using (SqlCommand command = new SqlCommand(updateQuery1, connection))
                        {
                            command.Parameters.AddWithValue("@MaNV", selectedEmployee.MaNV);
                            command.Parameters.AddWithValue("@DoanhSo", selectedEmployee.DoanhSo);
                            await command.ExecuteNonQueryAsync();
                        }
                    }
                    else if (selectedEmployee.NhanVien == "Giao nhận")
                    {
                        if (int.TryParse(txtPhuCap.Text, out int phucap))
                        {
                            selectedEmployee.PhuCap = phucap;
                        }
                        else
                        {
                            MessageBox.Show("Phụ cấp không hợp lệ!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }

                        using (SqlCommand command = new SqlCommand(updateQuery2, connection))
                        {
                            command.Parameters.AddWithValue("@MaNV", selectedEmployee.MaNV);
                            command.Parameters.AddWithValue("@PhuCap", selectedEmployee.PhuCap);
                            await command.ExecuteNonQueryAsync();
                        }
                    }

                }
                
            }
        }

        private async void btnSapXep_Click_1(object sender, RoutedEventArgs e)
        {
            List<ThongtinNhanVien> employees = lstNhanVien.Items.Cast<ThongtinNhanVien>().ToList();
            employees.Sort(new EmployeeComparer());

            lstNhanVien.Items.Clear(); // Xóa tất cả các mục hiện có trong ListView

            lstNhanVien.ItemsSource = employees;
            
        }

        private void btnThongKe_Click(object sender, RoutedEventArgs e)
        {
            int soNhanVienBanHang = 0;
            int soNhanVienGiaoNhan = 0;
            decimal tongLuongBanHang = 0;
            decimal tongLuongGiaoNhan = 0;

            string strConn = @"server=OYEEWIN\LECHIHAO;database=CSDL_QLNV;uid=sa;pwd=sa;Encrypt=False";

            // Khai báo danh sách employees
            List<ThongtinNhanVien> employees = new List<ThongtinNhanVien>();

            using (SqlConnection connection = new SqlConnection(strConn))
            {
                connection.Open();

                string selectQuery = "SELECT * FROM ThongtinNhanVien";

                using (SqlCommand command = new SqlCommand(selectQuery, connection))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ThongtinNhanVien employee = new ThongtinNhanVien
                        {
                            MaNV = reader["MaNV"].ToString(),
                            TenNV = reader["TenNV"].ToString(),
                            GioiTinh = reader["GioiTinh"].ToString(),
                            NgayVaoLam = Convert.ToDateTime(reader["NgayVaoLam"]),
                            DienThoai = reader["DienThoai"].ToString(),
                            DoanhSo = Convert.ToDecimal(reader["DoanhSo"]),
                            PhuCap = Convert.ToDecimal(reader["PhuCap"]),
                            NhanVien = reader["NhanVien"].ToString(),
                        };

                        employees.Add(employee); // Thêm nhân viên vào danh sách
                    }
                }
            }

            foreach (ThongtinNhanVien employee in employees)
            {
                if (employee.NhanVien == "Bán hàng")
                {
                    soNhanVienBanHang++;
                    // Tính lương bán hàng: Lương cơ bản + 10% doanh số
                    decimal luongBanHang = 7000000m + (employee.DoanhSo * 0.1m);
                    tongLuongBanHang += luongBanHang;
                }
                else if (employee.NhanVien == "Giao nhận")
                {
                    soNhanVienGiaoNhan++;
                    // Tính lương giao nhận: Lương cơ bản + phụ cấp nhiên liệu
                    decimal luongGiaoNhan = 7000000m + employee.PhuCap;
                    tongLuongGiaoNhan += luongGiaoNhan;
                }
            }

            // Hiển thị thông tin thống kê trong MessageBox
            string thongTinThongKe = $"Số nhân viên bán hàng: {soNhanVienBanHang}\n" +
                                     $"Số nhân viên giao nhận: {soNhanVienGiaoNhan}\n" +
                                     $"Tổng lương bán hàng: {tongLuongBanHang.ToString("C")}\n" +
                                     $"Tổng lương giao nhận: {tongLuongGiaoNhan.ToString("C")}";

            MessageBox.Show(thongTinThongKe, "Thống kê nhân viên", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
