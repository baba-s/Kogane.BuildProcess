using System;

namespace Kogane
{
    /// <summary>
    /// ビルドコマンドのリザルトタイプ
    /// </summary>
    public enum BuildCommandResultType
    {
        SUCCESS, // 成功
        WARNING, // 警告
        ERROR,   // エラー
    }

    /// <summary>
    /// BuildCommandResultType 型の拡張メソッドを管理するクラス
    /// </summary>
    public static class BuildCommandResultTypeExt
    {
        /// <summary>
        /// アッパーキャメルケースの文字列を返します
        /// </summary>
        public static string ToUpperCamel( this BuildCommandResultType self )
        {
            return self switch
            {
                BuildCommandResultType.SUCCESS => "Success",
                BuildCommandResultType.WARNING => "Warning",
                BuildCommandResultType.ERROR   => "Error",
                _                              => throw new ArgumentOutOfRangeException( nameof( self ), self, null )
            };
        }
    }
}