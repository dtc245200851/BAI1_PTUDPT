# Bài 1 – Tổng quan hệ thống và MEP

Họ tên: **Lê Hải Anh** — MSSV: **DTC245200851** — Lớp: **CNTT K23E**

Ngày kiểm tra: 04/11/2026.

Ghi chú trung thực: mã nguồn và bản nháp nhật ký được Codex hỗ trợ thực hiện.
Sinh viên cần tự chạy lại, thực hành debug và bổ sung ảnh của mình. Không coi
phần này là xác nhận đã tự gõ lại code theo yêu cầu bước 11.
Hướng dẫn ảnh: [CHUP-ANH.md](CHUP-ANH.md). Lưu ảnh trong `docs/images/`.

## 1. Cài đặt

- .NET SDK kiểm tra được: `10.0.403`.
- Git: `2.55.0.windows.5`.
- Chưa xác nhận extension VS Code và ảnh Hello World; sinh viên thực hiện theo hướng dẫn ảnh.

Ảnh cần bổ sung: `images/01-moi-truong.png`.

## 2. MepDemo (Bước 7)

Đã chạy cả năm mô hình từ dự án mẫu.

1. MEP 3: mỗi sub-x chỉ do **một** worker xử lý; hai worker chia công việc.
2. MEP 4: một sự kiện đến **ba** dịch vụ: ScoreService, NotificationService, StatsService.
3. Mô tả:
   - Request–response: bên gửi yêu cầu chờ bên nhận trả kết quả, có thể timeout.
   - One-way: gửi thông điệp rồi tiếp tục, không chờ kết quả nghiệp vụ.
   - Queue: công việc vào hàng đợi, mỗi công việc được một worker nhận trong demo.
   - Publish–subscribe: sự kiện được phát đến mọi bên đã đăng ký.
   - Streaming/push: nhận nhiều cập nhật liên tiếp mà không gửi yêu cầu mới cho từng cập nhật.

Ảnh cần bổ sung: `images/02-mep.png`.

## 3. BasicWebApi (Bước 8)

Đã bổ sung `language=ja` trả `Konnichiwa, An!`; kiểm tra cả `ja` và `JA`.
Đã kiểm tra GET 200, POST hợp lệ 201 có Location, tên rỗng 400.

1. `builder.Services.AddSingleton<GreetingService>()` phải trước `builder.Build()`.
   Build tạo service provider; cần đăng ký dịch vụ trước khi provider được tạo.
2. `name` lấy từ route `/hello/{name}`; `language` lấy từ query string `?language=en`.
3. POST tạo tài nguyên mới nên trả 201 Created, kèm Location chỉ URI tài nguyên;
   200 OK chỉ thể hiện xử lý thành công, không diễn đạt cụ thể việc tạo mới.

Ảnh cần bổ sung: `images/03-basic-200.png`, `04-basic-201.png`,
`05-basic-400.png`, `06-basic-ja.png`, `07-debug.png`.
Thao tác breakpoint/debug phải thực hiện trực tiếp trong IDE.

## 4. MiniDistributedSystem (Bước 9)

Đã đọc API và Client mẫu; các thí nghiệm dùng bản sao trong thư mục kiểm tra
`artifacts/experiments`, giữ nguyên nguồn mẫu. API và Client là hai tiến trình.

1. Client gọi `GET /api/assignments` → 200 với mảng bài tập, sau đó
   `POST /api/submissions` → 201 với JSON lượt nộp và header Location.
2. Hai project không tham chiếu nhau. Chúng thống nhất method, URL, mã trạng thái,
   Content-Type và cấu trúc JSON (tên trường, kiểu dữ liệu).
3. Truyền địa chỉ qua tham số giúp đổi máy chủ/cổng/môi trường mà không sửa và biên dịch code.

Ảnh cần bổ sung: `images/08-mini-api-client.png`.

## 5. Thí nghiệm lỗi (Bước 10)

Cột dự đoán dưới đây mô tả kiến thức kỳ vọng; sinh viên cần tự ghi dự đoán của mình
trước khi chạy lại. Kết quả thực tế là kết quả Codex kiểm tra trên bản sao code mẫu.
Dùng cổng 15050/15051 thay cho 5050/5000 để tránh tiến trình đang chạy.

| # | Dự đoán theo lý thuyết | Thực tế kiểm tra | Đúng/Sai | Giải thích |
|---|---|---|---|---|
| a | Client hiện thêm bài 3, không cần sửa | Hiện bài 1, 2, 3; nộp thành công, received | Khớp | Client đọc danh sách và lặp qua từng phần tử; cấu trúc JSON không đổi. |
| b | Tắt API làm kết nối thất bại | HttpRequestException, SocketException 10061, connection refused | Khớp | Không có máy chủ lắng nghe. Gọi mạng còn phụ thuộc kết nối, địa chỉ, tiến trình từ xa; gọi hàm cục bộ không có các rủi ro mạng này, nhưng vẫn có thể ném lỗi nghiệp vụ hoặc lập trình. |
| c | Mã sinh viên rỗng bị từ chối | POST trả 400; EnsureSuccessStatusCode ném HttpRequestException | Khớp | Hàm ném lỗi khi status ngoài 200–299. Máy chủ tự kiểm tra vì client có thể bị thay đổi hoặc gửi request trực tiếp. |
| d | Sai cổng thất bại; đổi tham số URL sẽ chạy được | Gọi 15051: connection refused; truyền http://localhost:15050: nhận danh sách và received | Khớp | Địa chỉ kết nối là cấu hình lúc chạy, không cần sửa mã nguồn. |

Ảnh khi chạy lại: `images/09-thi-nghiem.png` (có thể tách thành nhiều ảnh).

## 6. Dự án EduSubmit (Bước 11–12)

- Solution có Submission.Api và Submission.Client, .NET 10, không có ProjectReference.
- API có GET `/`, GET `/api/assignments`, POST `/api/submissions` và GET `/api/submissions/{id:guid}`.
- POST kiểm tra assignmentId > 0, studentCode và repositoryUrl không rỗng.
- Lượt nộp được lưu trong List, có khóa khi truy cập đồng thời; mất khi khởi động lại.
- GET lượt nộp trả 200 nếu tồn tại, 404 nếu không có.
- Client lấy URL từ args[0], mặc định localhost:5000; lấy bài tập, nộp và đọc lại lượt nộp.
- requests.http có 200, 201, các trường hợp 400 và 404; lấy ID tự động từ request tạo.
- Đã build cả hai project: 0 cảnh báo, 0 lỗi. Kiểm tra HTTP và chạy Client thực tế đều đạt.
- Phần tham khảo: Codex đã đọc code mẫu API/Client và MepDemo để đối chiếu đặc tả.

Luồng thông điệp (mỗi dòng là một cặp request–response):

```text
Client -- GET /api/assignments ---------> API -- 200 + JSON danh sách --> Client
Client -- POST /api/submissions --------> API -- 201 + Location/JSON --> Client
Client -- GET /api/submissions/{id} ----> API -- 200 + JSON lượt nộp --> Client
REST Client -- GET /api/submissions/{id không có} --> API -- 404 --> REST Client
```

Ảnh cần bổ sung: `images/10-edusubmit.png`, `11-doc-lai-200.png`, `12-khong-co-404.png`.

## 7. Khó khăn và cách giải quyết

- BasicWebApi.exe đang chạy khóa file khi build. Đã build project sang thư mục
  `artifacts/basic-check` để kiểm tra; khi chạy bình thường cần Ctrl+C terminal cũ rồi build lại.
- PowerShell chặn chạy file ps1 theo cấu hình máy; chạy script kiểm tra bằng
  `powershell -NoProfile -ExecutionPolicy Bypass -File .\verify.ps1` (chỉ áp dụng cho tiến trình đó).
- Không dùng ID cố định cho lượt nộp có thật: REST Client lấy ID từ response POST.
- Đã điền thông tin cá nhân và đẩy code lên https://github.com/dtc245200851/BAI1_PTUDPT.
  Còn bổ sung ảnh sinh viên trước khi commit cuối, gắn tag `bai1` và push GitHub theo README.

## 8. Tự kiểm tra

1. Hệ thống phân tán có các thành phần chạy độc lập, trao đổi qua mạng. Ranh giới ở đây
   là HTTP giữa tiến trình Client và API; monolithic thường gộp xử lý trong cùng ứng dụng.
2. Năm MEP được liệt kê ở mục 2; EduSubmit dùng request–response.
3. Queue chia công việc, ví dụ chấm từng bài nộp; pub/sub phát cùng sự kiện đến nhiều bên,
   ví dụ lưu điểm, gửi thông báo và cập nhật thống kê sau khi chấm.
4. 200: truy vấn thành công; 201: tạo tài nguyên; 400: đầu vào không hợp lệ;
   404: không tìm thấy tài nguyên/route.
5. Cần kiểm tra status để không giải mã JSON thông báo lỗi thành dữ liệu thành công.
