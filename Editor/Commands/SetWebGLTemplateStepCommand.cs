using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// WebGL テンプレートを設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetWebGLTemplateCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetWebGLTemplateCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly string m_template;

        //==============================================================================
        // 変数
        //==============================================================================
        private string m_oldTemplate;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetWebGLTemplateCommand( string template )
        {
            m_template = template;
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldTemplate                 = PlayerSettings.WebGL.template;
            PlayerSettings.WebGL.template = m_template;
            return Success( m_template );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.WebGL.template = m_oldTemplate;
        }
    }
}