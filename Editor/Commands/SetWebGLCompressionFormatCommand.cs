using Kogane;
using UnityEditor;

namespace Kogane
{
	/// <summary>
	/// WebGL の圧縮タイプを設定するコマンドを管理するクラス
	/// </summary>
	public sealed class SetWebGLCompressionFormatCommand : BuildCommandBase
	{
		//==============================================================================
		// プロパティ
		//==============================================================================
		public override string Tag => nameof( SetWebGLCompressionFormatCommand );

		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly WebGLCompressionFormat m_format;

		//==============================================================================
		// 変数
		//==============================================================================
		private WebGLCompressionFormat m_oldFormat;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SetWebGLCompressionFormatCommand( WebGLCompressionFormat format )
		{
			m_format = format;
		}

		/// <summary>
		/// 実行する時に呼び出されます
		/// </summary>
		protected override BuildCommandResult DoRun()
		{
			m_oldFormat                            = PlayerSettings.WebGL.compressionFormat;
			PlayerSettings.WebGL.compressionFormat = m_format;
			return Success( m_format.ToString() );
		}

		/// <summary>
		/// 後始末します
		/// </summary>
		protected override void DoCleanUp()
		{
			PlayerSettings.WebGL.compressionFormat = m_oldFormat;
		}
	}
}