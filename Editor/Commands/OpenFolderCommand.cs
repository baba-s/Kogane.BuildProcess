using System.Diagnostics;
using System.IO;

namespace Kogane
{
	/// <summary>
	/// 指定されたファイルが存在するフォルダを開くコマンドを管理するクラス
	/// </summary>
	public sealed class OpenFolderCommand : BuildCommandBase
	{
		//==============================================================================
		// プロパティ
		//==============================================================================
		public override string Tag => nameof( OpenFolderCommand );

		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly string m_locationPathName;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public OpenFolderCommand( string locationPathName )
		{
			m_locationPathName = locationPathName;
		}

		/// <summary>
		/// 実行する時に呼び出されます
		/// </summary>
		protected override BuildCommandResult DoRun()
		{
			var unityProjectPath = Directory.GetCurrentDirectory();
			var filePath         = unityProjectPath + "\\" + m_locationPathName;
			var folderPath       = Path.GetDirectoryName( filePath );

			if ( string.IsNullOrWhiteSpace( folderPath ) ) return Warning( folderPath );

			Process.Start( folderPath );

			return Success( folderPath );
		}
	}
}