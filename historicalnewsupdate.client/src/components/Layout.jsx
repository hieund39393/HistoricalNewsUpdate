import { Menu, Layout, Button, theme } from "antd";
import { NavLink, Outlet } from "react-router-dom";
import {
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    UserOutlined,
    HomeOutlined,
    LineChartOutlined,
    BankOutlined
} from '@ant-design/icons';
import { useState } from "react";
const { Header, Sider, Content } = Layout;

export default function CustomLayout() {
    const [collapsed, setCollapsed] = useState(false);
    const {
        token: { colorBgContainer, borderRadiusLG },
    } = theme.useToken();

    const testMenuItems = [
        {
            href: '/',
            title: 'Home',
            index: 0, // Add a unique key prop
        },
        {
            href: '/login',
            title: 'Login',
            index: 1, // Add a unique key prop
        }
    ];

    return (
        <Layout style={{ minHeight: '100vh' }}>
            <Sider width={270} trigger={null} collapsible collapsed={collapsed} className="fixed-menu">
                <div style={{ marginBottom: 50 }}>
                </div>
                <div className="demo-logo-vertical" />
                <Menu theme="dark" mode="inline">
                    {testMenuItems.map(({ href, title, index }) => (
                        <Menu.Item key={index} icon={<HomeOutlined />} >
                            <NavLink to={href}>{title}</NavLink>
                        </Menu.Item>
                    ))}
                </Menu>
            </Sider>
            <Layout>
                <Header
                    style={{
                        padding: 0,
                    }}
                >
                    <Button
                        type="text"
                        icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                        onClick={() => setCollapsed(!collapsed)}
                        style={{
                            fontSize: '16px',
                            width: 64,
                            height: 64,
                        }}
                    />
                </Header>
                <Content
                    style={{
                        margin: '24px 16px',
                        padding: 24,
                        minHeight: 280,
                        background: colorBgContainer,
                        borderRadius: borderRadiusLG,
                    }}
                >
                    <Outlet />
                </Content>
            </Layout>
        </Layout >
    );
}
