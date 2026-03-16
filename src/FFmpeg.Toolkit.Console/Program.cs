
using ManuHub.FFmpeg.Toolkit;

Console.WriteLine("Hello, World!");

var input = @"C:\Users\manua\Videos\Anu.mp4";
var output = @"C:\downloads\Output_Anu.mp4";

var progress = new Progress<FFmpegProgress>(p =>
{
    Console.WriteLine($"Frame: {p.Frame}  Time: {p.Time}  Speed: {p.Speed}x");
});

if (File.Exists("Tools\\ffmpeg.exe"))
{
    Console.WriteLine("FFmpeg Found!");
}

FFmpegToolkit.Configure(opt =>
{
    opt.FFmpegPath = @"Tools\ffmpeg.exe";
    opt.FFprobePath = @"Tools\ffprobe.exe";
    opt.TempDirectory = Path.Combine(Path.GetTempPath(), "MyAppFFmpeg");
    //opt.Logger = loggerFactory.CreateLogger("FFmpegToolkit");
});

// 2. Simple usage – no manual runner/options needed
var result = await FFmpegToolkit
    .Convert()
    .From(input)
    .To(output)
    .Scale(1280, 720)
    .Crf(22)
    .WithProgress(progress)
    .ExecuteAsync();

// Or thumbnails
//await FFmpegToolkit
//    .Thumbnails()
//    .From(input)
//    .Count(8)
//    .Tile(2, 4)
//    .To("thumbnails-grid.jpg")
//    .WithProgress(progress)
//    .ExecuteAsync();