using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Udemy.Spock.ParallelProgramming.MultiThreading
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SynchronizationContext _context;
        public MainWindow()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
        }

        private void FindBook_Click(object sender, RoutedEventArgs e)
        {
            Task.Factory.StartNew(() =>
            {
                _context.Send(state => FindBook.Content = "BOOM!!!", null);

                Book result = BookStorage.Find(9787532706068);
                Application.Current.Dispatcher.Invoke(() =>
                {
                    BookTitle.Text = result.Title;
                });
            });
        }
    }

    internal class BookStorage
    {
        internal static Book Find(long v)
        {
            //emulate long operation
            Thread.Sleep(5000);
            return new Book("Leo Tolstoy", "War and Pace");
        }
    }
    internal class Book
    {
        public string AuthorName { get; }
        public string Title { get; }

        public Book(string authorName, string title)
        {
            AuthorName = authorName;
            Title = title;

        }
    }

}
