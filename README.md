# EduSubmit – Bài 1

Yêu cầu .NET 10 SDK. Hai project độc lập, giao tiếp qua HTTP/JSON.
Mở terminal tại thư mục có `EduSubmit.slnx`.

```powershell
dotnet build
# Terminal 1
dotnet run --project src/Submission.Api --urls http://localhost:5000
# Terminal 2
dotnet run --project src/Submission.Client -- http://localhost:5000
```

Mở `requests.http` bằng REST Client. Chạy request `createSubmission` trước request
đọc lại để lấy ID tự động. API lưu dữ liệu trong RAM: khởi động lại sẽ mất các lượt nộp.
Client chạy lại sẽ tạo GUID mới. Đổi cổng bằng tham số dòng lệnh và biến `@api`.

Trước khi nộp: thay SV001 và URL ví dụ trong Client/requests.http, điền thông tin
trong `docs/bai1.md`, bổ sung ảnh theo `docs/CHUP-ANH.md`.
Code được hỗ trợ viết bằng Codex; cần tự đọc, chạy lại và thực hành debug theo đề.

Sau khi hoàn thiện ảnh và nhật ký, tạo repository GitHub trống EduSubmit-MSSV,
rồi chạy tại thư mục này (thay đường dẫn GitHub):

```powershell
git init
git add .
git commit -m "Bai 1: API, Client, mo rong va nhat ky thuc hanh"
git tag bai1
git branch -M main
git remote add origin https://github.com/TEN-TAI-KHOAN/EduSubmit-MSSV.git
git push -u origin main --tags
```

Chỉ gắn tag sau khi đã bổ sung ảnh để tag chứa đầy đủ bài nộp.
