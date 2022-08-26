using UnityEditor;
using UnityEngine;

namespace Kogane
{
    /// <summary>
    /// スタックトレースのオプションを設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetStackTraceLogTypeCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetStackTraceLogTypeCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly StackTraceLogType m_type;

        //==============================================================================
        // 変数
        //==============================================================================
        private StackTraceLogType m_oldType;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetStackTraceLogTypeCommand( StackTraceLogType type )
        {
            m_type = type;
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldType = PlayerSettings.GetStackTraceLogType( LogType.Log );

            SetStackTraceLogType( m_type );

            return Success( m_type.ToString() );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            SetStackTraceLogType( m_oldType );
        }

        /// <summary>
        /// すべてのログのスタックトレースのオプションを設定します
        /// </summary>
        private static void SetStackTraceLogType( StackTraceLogType type )
        {
            PlayerSettings.SetStackTraceLogType( LogType.Error, type );
            PlayerSettings.SetStackTraceLogType( LogType.Assert, type );
            PlayerSettings.SetStackTraceLogType( LogType.Warning, type );
            PlayerSettings.SetStackTraceLogType( LogType.Log, type );
            PlayerSettings.SetStackTraceLogType( LogType.Exception, type );
        }
    }
}