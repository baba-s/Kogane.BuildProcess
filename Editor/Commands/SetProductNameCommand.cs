using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// プロダクト名を設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetProductNameCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetProductNameCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly string m_productName;

        //==============================================================================
        // 変数
        //==============================================================================
        private string m_oldProductName;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetProductNameCommand( string productName )
        {
            m_productName = productName;
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldProductName           = PlayerSettings.productName;
            PlayerSettings.productName = m_productName;
            return Success( m_productName );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.productName = m_oldProductName;
        }
    }
}