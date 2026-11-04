# Hướng dẫn chụp ảnh minh chứng

Dùng Win+Shift+S, chụp đủ lệnh/URL, mã HTTP và JSON kết quả. Lưu PNG vào
`EduSubmit/docs/images/`. Không chỉ chụp code vì chưa chứng minh chạy được.
Ảnh thật chưa được tạo; các tên dưới đây là vị trí cần bổ sung.

## 1. Môi trường — 01-moi-truong.png

Terminal tại BasicWebApi:

```powershell
dotnet --version
git --version
dotnet new console -n HelloPtudpt -o artifacts/HelloPtudpt
dotnet run --project artifacts/HelloPtudpt
```

Chụp thấy phiên bản 10.0, Git và `Hello, World!`. Kiểm tra Extensions có C# Dev Kit,
C#, .NET Install Tool, REST Client; chụp thêm nếu giảng viên yêu cầu.

## 2. MEP — 02-mep.png

Từ BasicWebApi, chạy `dotnet run --project ../MepDemo`. Chụp phần queue thấy mỗi sub
do một worker xử lý và pub/sub thấy ba dịch vụ nhận sự kiện (tách hai ảnh nếu cần).

## 3. BasicWebApi — ảnh 03 đến 07

Dừng API cũ bằng Ctrl+C ở terminal đang chạy trước khi build để tránh khóa file.
Từ thư mục BasicWebApi:

```powershell
dotnet build
dotnet run --project src/BasicWebApi --urls http://localhost:5081
```

Mở requests.http ở thư mục BasicWebApi, bấm Send Request và chụp cả request + response:

| Tên ảnh | Thao tác | Phải thấy |
|---|---|---|
| 03-basic-200.png | GET / | 200, application BasicWebApi |
| 04-basic-201.png | POST tên Hanh | 201, Location, JSON id/name |
| 05-basic-400.png | POST tên rỗng | 400, Name must not be empty |
| 06-basic-ja.png | GET /hello/Hai_Anh?language=ja | 200, Konnichiwa |
| 07-debug.png | Dừng API; breakpoint tại service.Create; Debug project, gửi POST | Dừng tại breakpoint, xem request.Name trong Variables/hover |

Nếu debug chạy ở cổng khác, sửa biến @api đúng cổng terminal thông báo.
F10 chạy từng dòng, F5 tiếp tục, Shift+F5 dừng debug.

## 4. Mẫu MiniDistributedSystem — 08-mini-api-client.png

Hai terminal từ BasicWebApi:

```powershell
# Terminal 1
dotnet run --project ../MiniDistributedSystem/src/Submission.Api --urls http://localhost:5000
# Terminal 2
dotnet run --project ../MiniDistributedSystem/src/Submission.Client -- http://localhost:5000
```

Chụp hai terminal cạnh nhau: API listening và Client hiện hai bài, GUID, received.
Dừng API bằng Ctrl+C trước khi chạy EduSubmit cùng cổng.

## 5. Thí nghiệm — 09-thi-nghiem-a/b/c/d.png

Làm trên MiniDistributedSystem; ghi dự đoán trước, hoàn nguyên sau mỗi thí nghiệm:

- a: thêm Assignment thứ ba ở API, khởi động lại; chạy Client thấy ba bài.
- b: tắt API, chạy Client; chụp HttpRequestException/connection refused.
- c: API chạy; đổi StudentCode trong Client thành chuỗi rỗng; chụp lỗi 400 từ EnsureSuccessStatusCode.
- d: API chạy `--urls http://localhost:5050`; chạy Client gọi 5000 để thấy lỗi,
  sau đó chạy lại với `-- http://localhost:5050` để thấy thành công. Không sửa code.

## 6. EduSubmit — ảnh 10 đến 12

Mở thư mục EduSubmit trong VS Code. Hai terminal:

```powershell
# Terminal 1
dotnet build
dotnet run --project src/Submission.Api --urls http://localhost:5000
# Terminal 2
dotnet run --project src/Submission.Client -- http://localhost:5000
```

- 10-edusubmit.png: hai terminal, danh sách bài, GUID, received và dòng Doc lai luot nop.
- 11-doc-lai-200.png: mở EduSubmit/requests.http, gửi createSubmission rồi request đọc lại;
  chụp 200 và GUID khớp response tạo. Có thể chụp thêm POST 201 và POST 400 của EduSubmit.
- 12-khong-co-404.png: gửi GET GUID toàn số 0; chụp 404.

## 7. Chèn ảnh và nộp

Trong bai1.md, thay từng dòng “Ảnh cần bổ sung” bằng Markdown tương ứng, ví dụ:

```markdown
![API và Client EduSubmit](images/10-edusubmit.png)
```

Đã điền họ tên/MSSV/lớp và repository thật trong Client/requests.http. Tự đọc và
chạy lại code theo yêu cầu tự làm của đề. Làm theo README để commit, tag bai1 và push.
Chụp thêm trang GitHub có code, docs và tag bai1 nếu cần minh chứng nộp bài.
