using System.Net.Http.Json;

var apiBaseUrl = args.Length > 0 ? args[0] : "http://localhost:5000";
using var http = new HttpClient { BaseAddress = new Uri(apiBaseUrl) };

var assignments = await http.GetFromJsonAsync<List<Assignment>>("/api/assignments");
Console.WriteLine("Danh sach bai tap:");
foreach (var assignment in assignments ?? []) {
    Console.WriteLine($"- {assignment.Id}: {assignment.Title}");
}

// Thay SV001 và URL repository bằng thông tin thật trước khi nộp bài.
var request = new CreateSubmissionRequest(1, "SV001", "https://example.com/sv001/bai-tap-1");
using var response = await http.PostAsJsonAsync("/api/submissions", request);
response.EnsureSuccessStatusCode();
var result = await response.Content.ReadFromJsonAsync<SubmissionResult>()
    ?? throw new InvalidOperationException("API tra ve du lieu rong.");
Console.WriteLine($"Da nop bai. Ma luot nop: {result.Id}");
Console.WriteLine($"Trang thai: {result.Status}");

var saved = await http.GetFromJsonAsync<SubmissionResult>($"/api/submissions/{result.Id}")
    ?? throw new InvalidOperationException("Khong doc duoc luot nop vua tao.");
Console.WriteLine($"Doc lai luot nop: {saved.Id}, SV: {saved.StudentCode}, Trang thai: {saved.Status}");

// Client tự khai báo hợp đồng JSON, không tham chiếu project API.
record Assignment(int Id, string Title, string Description);
record CreateSubmissionRequest(int AssignmentId, string StudentCode, string RepositoryUrl);
record SubmissionResult(Guid Id, int AssignmentId, string StudentCode,
    string Status, DateTimeOffset ReceivedAt);
