using System.Linq;
using UnityEditor;

namespace Kogane
{
	/// <summary>
	/// Preload Assets が正常かどうか検証するコマンドを管理するクラス
	/// </summary>
	public sealed class ValidatePreloadAssetsCommand : BuildCommandBase
	{
		//==============================================================================
		// プロパティ
		//==============================================================================
		public override string Tag => nameof( ValidatePreloadAssetsCommand );

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// 実行する時に呼び出されます
		/// </summary>
		protected override BuildCommandResult DoRun()
		{
			var hasMissing = PlayerSettings
					.GetPreloadedAssets()
					.Any( x => x == null )
				;

			return hasMissing ? Error() : Success();
		}
	}
}