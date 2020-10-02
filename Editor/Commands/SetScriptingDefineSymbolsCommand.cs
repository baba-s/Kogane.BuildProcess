using UnityEditor;

namespace Kogane
{
	/// <summary>
	/// シンボルを設定するコマンドを管理するクラス
	/// </summary>
	public sealed class SetScriptingDefineSymbolsCommand : BuildCommandBase
	{
		//==============================================================================
		// プロパティ
		//==============================================================================
		public override string Tag => nameof( SetScriptingDefineSymbolsCommand );

		//==============================================================================
		// 変数(readonly)
		//==============================================================================
		private readonly BuildTargetGroup m_targetGroup;
		private readonly string           m_defines;
		private readonly bool             m_isSkip;

		//==============================================================================
		// 変数
		//==============================================================================
		private string m_oldSymbols;

		//==============================================================================
		// 関数
		//==============================================================================
		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SetScriptingDefineSymbolsCommand
		(
			BuildTargetGroup targetGroup,
			string           defines
		)
		{
			m_targetGroup = targetGroup;
			m_defines     = defines;
			m_isSkip      = defines == null;
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SetScriptingDefineSymbolsCommand
		(
			BuildTarget target,
			string      defines
		) : this( BuildPipeline.GetBuildTargetGroup( target ), defines )
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SetScriptingDefineSymbolsCommand
		(
			BuildTargetGroup targetGroup,
			string[]         defines
		) : this( targetGroup, string.Join( ";", defines ) )
		{
		}

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public SetScriptingDefineSymbolsCommand
		(
			BuildTarget target,
			string[]    defines
		) : this( BuildPipeline.GetBuildTargetGroup( target ), defines )
		{
		}

		/// <summary>
		/// 実行する時に呼び出されます
		/// </summary>
		protected override BuildCommandResult DoRun()
		{
			if ( m_isSkip ) return Success( "Skip" );

			m_oldSymbols = PlayerSettings.GetScriptingDefineSymbolsForGroup( m_targetGroup );
			PlayerSettings.SetScriptingDefineSymbolsForGroup( m_targetGroup, m_defines );

			var message = string.Join( "\n", m_defines.Split( ';' ) );

			return Success( $"\n\n{message}" );
		}

		/// <summary>
		/// 後始末します
		/// </summary>
		protected override void DoCleanUp()
		{
			if ( m_isSkip ) return;

			PlayerSettings.SetScriptingDefineSymbolsForGroup( m_targetGroup, m_oldSymbols );
		}
	}
}