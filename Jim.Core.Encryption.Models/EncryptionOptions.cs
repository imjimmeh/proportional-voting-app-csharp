using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jim.Core.Encryption.Models
{
    public record EncryptionOptions
    {
        public EncryptionOptions()
        {
        }

        public EncryptionOptions(string secret)
        {
            Secret = secret ?? throw new ArgumentNullException(nameof(secret));
        }

        public string Secret { get; init; } = null!;
    }
}
