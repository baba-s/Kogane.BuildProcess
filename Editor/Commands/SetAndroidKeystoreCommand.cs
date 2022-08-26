using UnityEditor;

namespace Kogane
{
    /// <summary>
    /// Android の Keystore の情報を設定するコマンドを管理するクラス
    /// </summary>
    public sealed class SetAndroidKeystoreCommand : BuildCommandBase
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        public override string Tag => nameof( SetAndroidKeystoreCommand );

        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly string m_keystoreName;
        private readonly string m_keystorePass;
        private readonly string m_keyaliasName;
        private readonly string m_keyaliasPass;

        //==============================================================================
        // 変数
        //==============================================================================
        private string m_oldKeystoreName;
        private string m_oldKeystorePass;
        private string m_oldKeyaliasName;
        private string m_oldKeyaliasPass;

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SetAndroidKeystoreCommand
        (
            string keystoreName,
            string keystorePass,
            string keyaliasName,
            string keyaliasPass
        )
        {
            m_keystoreName = keystoreName;
            m_keystorePass = keystorePass;
            m_keyaliasName = keyaliasName;
            m_keyaliasPass = keyaliasPass;
        }

        /// <summary>
        /// 実行する時に呼び出されます
        /// </summary>
        protected override BuildCommandResult DoRun()
        {
            m_oldKeystoreName = PlayerSettings.Android.keystoreName;
            m_oldKeystorePass = PlayerSettings.Android.keystorePass;
            m_oldKeyaliasName = PlayerSettings.Android.keyaliasName;
            m_oldKeyaliasPass = PlayerSettings.Android.keyaliasPass;

            PlayerSettings.Android.keystoreName = m_keystoreName;
            PlayerSettings.Android.keystorePass = m_keystorePass;
            PlayerSettings.Android.keyaliasName = m_keyaliasName;
            PlayerSettings.Android.keyaliasPass = m_keyaliasPass;

            return Success( m_keystoreName );
        }

        /// <summary>
        /// 後始末します
        /// </summary>
        protected override void DoCleanUp()
        {
            PlayerSettings.Android.keystoreName = m_oldKeystoreName;
            PlayerSettings.Android.keystorePass = m_oldKeystorePass;
            PlayerSettings.Android.keyaliasName = m_oldKeyaliasName;
            PlayerSettings.Android.keyaliasPass = m_oldKeyaliasPass;
        }
    }
}