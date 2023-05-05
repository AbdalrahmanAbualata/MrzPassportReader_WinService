
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;


namespace IdCardReader_WinService.Modilty
{

    /// <summary>
    /// Image
    /// </summary>
    [DataContract(Name = "Image")]
    public class Image
    {
      
        /// <summary>
        /// Initializes a new instance of the <see cref="Image" /> class.
        /// </summary>
        /// <param name="captureDevice">captureDevice.</param>
        /// <param name="dataBytes">dataBytes.</param>
        /// <param name="dataUrl">dataUrl.</param>
        /// <param name="format">Enumeration of image format..</param>
        /// <param name="resolutionDpi">resolutionDpi.</param>
        public Image(string captureDevice = default(string), byte[] dataBytes = default(byte[]))
        {
            this.DataBytes = dataBytes;
        }


        /// <summary>
        /// Gets or Sets DataBytes
        /// </summary>
        [DataMember(Name = "dataBytes", EmitDefaultValue = false)]
        public byte[] DataBytes { get; set; }

    

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("class Image {\n");
            sb.Append("  DataBytes: ").Append(DataBytes).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }

        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented);
        }
        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        public IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
