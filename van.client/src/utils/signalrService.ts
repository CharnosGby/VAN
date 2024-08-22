import { HubConnection, HubConnectionBuilder, LogLevel } from '@microsoft/signalr';
const BASEURL = 'http://localhost:8081'
let connection: HubConnection | null = null;

export function createConnection(url:string): HubConnection {
    if (!connection) {
        connection = new HubConnectionBuilder()
            .withUrl(`${BASEURL}/${url}`) // 替换为你的 SignalR Hub 地址
            .configureLogging(LogLevel.Information)
            .build();
    }
    return connection;
}

export async function startConnection(url:string): Promise<HubConnection> {
    const conn = createConnection(url);
    try {
        await conn.start();
        console.log('SignalR 连接成功.');
    } catch (err) {
        console.error('SignalR 连接错误: ', err);
    }
    return conn;
}