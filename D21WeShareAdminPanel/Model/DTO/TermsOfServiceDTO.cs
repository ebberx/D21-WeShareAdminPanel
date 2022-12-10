using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D21WeShareAdminPanel.Model.DTO
{
    public class TermsOfServiceDTO {
        public int toSid { get; set; }
        public string? creationDate { get; set; }
        public string? lastModificationDate { get; set; }
        public string? content { get; set; }
    }
}
