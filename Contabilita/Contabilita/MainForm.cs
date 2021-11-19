using Infrastructure.Dal;
using Microsoft.Extensions.Logging;

namespace Contabilita
{
    public partial class MainForm : Form
    {
        public readonly IContabilitaDal _contabilitaDal;
        public readonly ILogger<MainForm> logger;


        public MainForm(IContabilitaDal contabilitaDal, ILogger<MainForm> logger)
        {
            _contabilitaDal = contabilitaDal;
            this.logger = logger;
            InitializeComponent();
        }
    }
}