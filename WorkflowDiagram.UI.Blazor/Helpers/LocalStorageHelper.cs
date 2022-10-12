// LocalStorage.cs
using System;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace WorkflowDiagram.UI.Blazor.Helpers {

	public interface ILocalStorage {
		public Task RemoveAsync(string key);
		public Task SaveStringAsync(string key, string value);
		public Task<string> GetStringAsync(string key);
		public Task SaveStringArrayAsync(string key, string[] values);
		public Task<string[]> GetStringArrayAsync(string key);
	}

	public class LocalStorage : ILocalStorage {
		private readonly IJSRuntime jsruntime;
		public LocalStorage(IJSRuntime jSRuntime) {
			jsruntime = jSRuntime;
		}

		public async Task RemoveAsync(string key) {
			await jsruntime.InvokeVoidAsync("localStorage.removeItem", key).ConfigureAwait(false);
		}

		public async Task SaveStringAsync(string key, string value) {
			var compressedBytes = await Compressor.CompressBytesAsync(Encoding.UTF8.GetBytes(value));
			await jsruntime.InvokeVoidAsync("localStorage.setItem", key, Convert.ToBase64String(compressedBytes)).ConfigureAwait(false);
		}

		public async Task<string> GetStringAsync(string key) {
			var str = await jsruntime.InvokeAsync<string>("localStorage.getItem", key).ConfigureAwait(false);
			if(str == null)
				return null;
			var bytes = await Compressor.DecompressBytesAsync(Convert.FromBase64String(str));
			return Encoding.UTF8.GetString(bytes);
		}

		public async Task SaveStringArrayAsync(string key, string[] values) {
			await SaveStringAsync(key, values == null ? "" : string.Join('\0', values));
		}

		public async Task<string[]> GetStringArrayAsync(string key) {
			var data = await GetStringAsync(key);
			if(!string.IsNullOrEmpty(data))
				return data.Split('\0');
			return null;
		}
	}

	internal class Compressor {
		public static async Task<byte[]> CompressBytesAsync(byte[] bytes) {
			using(var outputStream = new MemoryStream()) {
				using(var compressionStream = new GZipStream(outputStream, CompressionLevel.Optimal)) {
					await compressionStream.WriteAsync(bytes, 0, bytes.Length);
				}
				return outputStream.ToArray();
			}
		}

		public static async Task<byte[]> DecompressBytesAsync(byte[] bytes) {
			using(var inputStream = new MemoryStream(bytes)) {
				using(var outputStream = new MemoryStream()) {
					using(var compressionStream = new GZipStream(inputStream, CompressionMode.Decompress)) {
						await compressionStream.CopyToAsync(outputStream);
					}
					return outputStream.ToArray();
				}
			}
		}
	}
}
