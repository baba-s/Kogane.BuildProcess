namespace Kogane
{
    /// <summary>
    /// ビルドコマンドのリザルトを管理する構造体
    /// </summary>
    public readonly struct BuildCommandResult
    {
        //==============================================================================
        // プロパティ
        //==============================================================================
        /// <summary>
        /// タグを返します
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// リザルトタイプを返します
        /// </summary>
        public BuildCommandResultType Type { get; }

        /// <summary>
        /// メッセージを返します
        /// </summary>
        public string Message { get; }

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BuildCommandResult
        (
            string                 tag,
            BuildCommandResultType type,
            string                 message
        )
        {
            Tag     = tag;
            Message = message;
            Type    = type;
        }
    }
}