using System.Collections.Generic;
using ContabilitaWeb.Models.TableToExport;

namespace ContabilitaWeb.Models.Interfaces
{
    /// <summary>
    /// Tutte le tabelle che prevedono esportazione devono implementare questa classe
    /// </summary>
    public interface IConvertToTableExport
    {
        TableExport ConvertToTabellaExport();
        TableExport ConvertToTabellaExport(List<string> Colonne);
    }
}
