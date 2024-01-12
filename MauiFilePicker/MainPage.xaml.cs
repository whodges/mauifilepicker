namespace MauiFilePicker
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnOpenClickedAsync(object sender, EventArgs e)
        {
            // A simple way to create a plethora of text files to open
            //const string TestTextFileFolder = "c:\\textfiles";
            //if (Directory.Exists(TestTextFileFolder) && !Directory.EnumerateFileSystemEntries(TestTextFileFolder).Any())
            //    CreateTestTextFiles(TestTextFileFolder);



            var filePickerResults = await FilePicker.PickMultipleAsync();
            if (filePickerResults == null || !filePickerResults.Any())
            {
                await DisplayAlert("Notification", "No files were selected.", "OK");
                return;
            }

            foreach (var result in filePickerResults)
            {
                try
                {
                    using (var fileStream = new FileStream(result.FullPath, FileMode.Open, FileAccess.Read))
                    {
                        fileStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Error", ex.Message, "OK");
                    return;
                }
            }

            await DisplayAlert("Notification", "All files were opened and closed successfully.", "OK");
        }

        private void CreateTestTextFiles(string folder)
        {
            const int FileCount = 1000;     // I made this 1000, but I found typically around 200 will do

            for (var i = 0; i < FileCount; i++)
            {
                var filename = i.ToString("D4") + ".txt";
                var fullPath = folder + "/" + filename;

                using (var streamWriter = new StreamWriter(fullPath))
                {
                    streamWriter.WriteLine("test file");
                    streamWriter.Close();
                }
            }
        }
    }

}
