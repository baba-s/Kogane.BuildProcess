using UnityEditor;

namespace Kogane
{
	/// <summary>
	/// 後始末する時に AssetDatabase.SaveAssets を実行するコマンドを管理するクラス
	/// </summary>
	public sealed class SaveAssetsOnCleanUpCommand : BuildCommandBase
	{
		//==============================================================================
		// プロパティ
		//==============================================================================
		public override string Tag => nameof( SaveAssetsOnCleanUpCommand );

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// 実行する時に呼び出されます
		/// </summary>
		protected override BuildCommandResult DoRun()
		{
			return Success();
		}

		/// <summary>
		/// 後始末する時に呼び出されます
		/// </summary>
		protected override void DoCleanUp()
		{
			AssetDatabase.SaveAssets();
		}
	}
}