Họ và tên: Trần Thế Hào
Lớp: D19QTANM1
Mã sinh viên: 24810320120
Môn: Lập trình.NET

BÀI KIỂM TRA SỐ 1 (LÝ THUYẾT)
Câu 1: Sự khác nhau giữa Value Types và Reference Types (Stack vs Heap)
1. Value Types (Kiểu giá trị):
• Cơ chế lưu trữ: Biến lưu trữ trực tiếp giá trị của nó. Nếu là biến cục bộ hoặc tham số, nó nằm trực tiếp trên Stack. Nếu là trường (field) của một kiểu tham chiếu, nó được lưu nội tuyến (inline) bên trong vùng nhớ của đối tượng đó trên Heap.
• Ví dụ phổ biến: Các kiểu nguyên thủy (int, float, bool, char), struct, enum.
• Hành vi gán: Khi gán một biến kiểu giá trị cho biến khác (b = a), một bản sao hoàn toàn mới của giá trị được tạo ra. Thay đổi trên biến này không ảnh hưởng đến biến kia.
2. Reference Types (Kiểu tham chiếu):
• Cơ chế lưu trữ: Biến chỉ lưu giữ địa chỉ tham chiếu (con trỏ/địa chỉ bộ nhớ) trỏ đến vùng dữ liệu thực sự nằm trên Heap. Vùng nhớ trên Stack (hoặc một đối tượng khác) chứa tham chiếu, còn đối tượng chính được lưu trên Heap.
• Ví dụ phổ biến: class, string, array, delegate, interface.
• Hành vi gán: Khi gán (b = a), chỉ có địa chỉ tham chiếu được sao chép. Cả hai biến cùng trỏ đến một vùng nhớ duy nhất trên Heap, nên thay đổi trên dữ liệu của đối tượng qua biến này sẽ làm ảnh hưởng đến biến kia.
Câu 2: Tính năng Init-only Properties (init) trong C# 9/10 so với set thông thường
1. Khác biệt cốt lõi:
• Accessor set: Cho phép gán lại giá trị bất cứ lúc nào trong suốt vòng đời của đối tượng.
• Accessor init: Chỉ cho phép gán giá trị trong quá trình khởi tạo đối tượng (thông qua Object Initializer hoặc bên trong constructor). Sau khi khởi tạo xong, thuộc tính trở thành bất biến (read-only) và không thể thay đổi giá trị nữa.
2. Trường hợp sử dụng thực tế:
Rất hữu ích khi xây dựng các đối tượng Immutable (bất biến) — nghĩa là dữ liệu không được phép thay đổi sau khi đã tạo ra (ví dụ: DTOs, cấu hình ứng dụng, record). Nó giúp mã nguồn an toàn hơn, tránh tình trạng vô tình làm thay đổi trạng thái của đối tượng ở các tầng xử lý khác.

public class UserDto
{
    public string Username { get; init; }
    public string Email { get; init; }
}

// Sử dụng:
var user = new UserDto { Username = "Admin", Email = "admin@test.com" };
// user.Username = "NewUser"; // Lỗi biên dịch! Không thể gán lại sau khi khởi tạo.
Câu 3: Phân biệt virtual ở lớp cha và override ở lớp con trong Đa hình
1. Từ khóa virtual (tại lớp cha):
Được đặt trước phương thức của lớp cha nhằm cho phép các lớp con có thể ghi đè (override) lại phương thức đó. Bản thân lớp cha vẫn cung cấp một cài đặt mặc định (default implementation).
2. Từ khóa override (tại lớp con):
Được đặt trước phương thức của lớp con để thay thế hoặc mở rộng phương thức virtual (hoặc abstract) tương ứng từ lớp cha.
3. Cơ chế Đa hình (Polymorphism):
Khi gọi phương thức thông qua một tham chiếu kiểu lớp cha nhưng trỏ đến một thể hiện của lớp con (ví dụ: Animal a = new Dog(); a.MakeSound();), CLR sẽ kiểm tra kiểu thực tế tại thời điểm chạy (runtime) và tự động thực thi phương thức override ở lớp con thay vì phương thức virtual của lớp cha.
Câu 4: Tại sao thành phần static không thể truy xuất qua object instance (new)?
1. Bản chất của static:
Các thành phần được khai báo là static (biến, phương thức) thuộc về chính lớp đó (Class) chứ không thuộc về bất kỳ thể hiện (object instance) cụ thể nào được tạo ra bởi từ khóa new. Chúng được nạp vào bộ nhớ ngay khi lớp được khởi tạo lần đầu tiên.
2. Lý do thiết kế:
• Quản lý bộ nhớ: Các thể hiện (instance) nằm trên Heap và chứa trạng thái riêng biệt của từng đối tượng. Trong khi đó, thành phần static tồn tại độc lập và chỉ có duy nhất một bản sao chung cho toàn bộ chương trình.
• Tránh nhầm lẫn ngữ cảnh: C# bắt buộc phải truy xuất qua tên lớp (ClassName.StaticMember) nhằm giúp lập trình viên phân biệt rõ ràng đâu là hành vi/dữ liệu gắn liền với đối tượng cụ thể, đâu là hành vi/dữ liệu dùng chung ở cấp độ lớp.
