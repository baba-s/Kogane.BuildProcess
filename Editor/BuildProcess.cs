using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Kogane
{
    /// <summary>
    /// 指定されたビルドコマンドを順番に実行するクラス
    /// </summary>
    public class BuildProcess
        : IDisposable,
          IEnumerable
    {
        //==============================================================================
        // 変数(readonly)
        //==============================================================================
        private readonly List<IBuildCommand> m_commands = new();

        //==============================================================================
        // 関数
        //==============================================================================
        /// <summary>
        /// ビルドコマンドを追加します
        /// </summary>
        public BuildProcess Add( IBuildCommand command )
        {
            if ( command == null ) return this;
            m_commands.Add( command );
            return this;
        }

        /// <summary>
        /// すべてのビルドコマンドを破棄します
        /// </summary>
        public void Dispose()
        {
            m_commands.Clear();
        }

        /// <summary>
        /// すべてのビルドコマンドを順番に実行します
        /// </summary>
        public virtual BuildCommandResult Run()
        {
            var stringBuilder     = new StringBuilder();
            var currentIndex      = -1;
            var processResultType = BuildCommandResultType.SUCCESS;

            stringBuilder.AppendLine( "--------------------[ Build Start ]--------------------" );
            stringBuilder.AppendLine();

            for ( var i = 0; i < m_commands.Count; i++ )
            {
                var command = m_commands[ i ];
                var tag     = command.Tag;

                stringBuilder.AppendLine( $"[{GetNowTimeString()}][{tag}] Start" );

                var commandResult     = command.Run();
                var commandResultType = commandResult.Type;

                stringBuilder.AppendLine( $"[{GetNowTimeString()}][{tag}] {commandResultType.ToUpperCamel()} : {commandResult.Message}" );

                if ( processResultType < commandResultType )
                {
                    processResultType = commandResultType;
                }

                currentIndex = i;

                if ( commandResultType == BuildCommandResultType.ERROR ) break;
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine( "--------------------[ Build End ]--------------------" );
            stringBuilder.AppendLine();
            stringBuilder.AppendLine( "--------------------[ Clean Up Start ]--------------------" );
            stringBuilder.AppendLine();

            for ( var i = currentIndex; 0 <= i; i-- )
            {
                var command = m_commands[ i ];
                var tag     = command.Tag;
                stringBuilder.AppendLine( $"[{GetNowTimeString()}][{tag}] Clean Up" );
                command.CleanUp();
            }

            stringBuilder.AppendLine();
            stringBuilder.AppendLine( "--------------------[ Clean Up End ]--------------------" );

            return new BuildCommandResult
            (
                tag: string.Empty,
                type: processResultType,
                message: stringBuilder.ToString()
            );
        }

        /// <summary>
        /// 現在時刻を表す文字列を返します
        /// </summary>
        private string GetNowTimeString()
        {
            return DateTime.Now.ToString( "HH:mm:ss" );
        }

        /// <summary>
        /// コレクションを反復処理する列挙子を返します
        /// </summary>
        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}