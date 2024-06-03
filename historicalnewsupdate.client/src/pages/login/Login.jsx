import { Form, Input, Spin, Button, Image } from 'antd';
import './login.css';
import { useNavigate } from 'react-router-dom';
import styled from 'styled-components';
import { useState } from 'react';
import { postData } from '../../utils/request';
import backgroundImage from '../../assets/login_background.svg';
import eurolandLogo from '../../assets/EurolandLogo_Blue.svg';
import { Endpoint } from '../../utils/endpoint';

export default function Login() {
    const [loading, setLoading] = useState(false);
    const [form] = Form.useForm();

    var token = localStorage.getItem('accessToken');
    var navigate = useNavigate();

    const onFinish = (values) => {
        postData({
            url: `${Endpoint.LOGIN}`,
            method: 'POST',
            payload: {
                ...values,
            },
            setLoading,
            onSuccess: (res) => {
                if (res.statusCode === 200 && res.data) {
                    localStorage.setItem('accessToken', res.data.accessToken);
                    navigate('/');
                } else {
                    getErrorForm(res, form);
                }
            },
        });
    };



    return token != null && token != undefined ? (
        window.location.href = `${window.location.origin}`
    ) : (
        <div
            style={{
                backgroundImage: `url(${backgroundImage})`,
                backgroundRepeat: 'no-repeat',

                backgroundSize: 'cover',
            }}
        >
            <div className="login-page">
                <Spin spinning={loading}>
                    <div className="login-box">
                        <Form form={form} layout="vertical" autoComplete="off" name="login-form" onFinish={onFinish}>
                            <p className="form-title" style={{ textAlign: 'center' }}>
                                <Image  src={eurolandLogo} preview={false} />
                            </p>
                            <p></p>
                            <Form.Item
                                name="userName"
                                rules={[{ required: true, message: 'Please input email!' }]}
                            >
                                <Input
                                    placeholder="Email"
                                    style={{
                                        fontSize: '18px',
                                        height: 70,
                                    }}
                                />
                            </Form.Item>

                            <Form.Item name="password" rules={[{ required: true, message: 'Please input password!' }]}>
                                <Input.Password style={{ fontSize: '18px', height: 70 }} placeholder="Password" />
                            </Form.Item>

                            <p></p>

                            <Button
                                type="primary"
                                htmlType="submit"
                                className="login-form-button"
                            // disabled={loading || disableSubmit}
                            >
                                LOGIN
                            </Button>
                        </Form>
                    </div>
                </Spin>
            </div>
        </div>
    );
}

const ButtonSSO = styled(Button)`
    margin-top: 15px;
    width: 100%;
    background-color: #ff5247 !important;
    border-color: #ff5247 !important;
    :hover {
        background-color: #ff5247;
        border-color: #ff5247;
        opacity: 0.9;
    }
`;