using System;

namespace SignUp.Abstractions
{
    /// <summary>
    /// Row data.
    /// </summary>
	public abstract class RowData
	{
		public string Id { get; set; }
		public DateTimeOffset? UpdatedAt { get; set; }
		public DateTimeOffset? CreatedAt { get; set; }
		public byte[] Version { get; set; }
	}
}
