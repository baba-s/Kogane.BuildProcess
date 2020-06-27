using UnityEditor;

namespace Kogane
{
	/// <summary>
	/// Android の targetArchitectures を設定するコマンドを管理するクラス
	/// </summary>
	public sealed class SetAndroidTargetArchitecturesCommand : BuildCommandBase
	{
		//==============================================================================
		// プロパティ
		//==============================================================================
		public override string Tag => nameof( SetAndroidTargetArchitecturesCommand );

		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly AndroidArchitecture m_targetArchitectures;

		//==============================================================================
		// 変数
		//==============================================================================
		private AndroidArchitecture m_oldTargetArchitectures;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SetAndroidTargetArchitecturesCommand
		(
			AndroidArchitecture targetArchitectures
		)
		{
			m_targetArchitectures = targetArchitectures;
		}

		/// <summary>
		/// 実行する時に呼び出されます
		/// </summary>
		protected override BuildCommandResult DoRun()
		{
			m_oldTargetArchitectures                   = PlayerSettings.Android.targetArchitectures;
			PlayerSettings.Android.targetArchitectures = m_targetArchitectures;
			return Success( m_targetArchitectures.ToString() );
		}

		/// <summary>
		/// 後始末します
		/// </summary>
		protected override void DoCleanUp()
		{
			PlayerSettings.Android.targetArchitectures = m_oldTargetArchitectures;
		}
	}
}