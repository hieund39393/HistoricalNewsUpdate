import {
    METHOD_DELETE,
    METHOD_GET,
    METHOD_POST,
    STATUSCODE_200,
    STATUSCODE_401,
    STATUSCODE_500,
    TOKEN_NAME,
} from './constants';
import { Modal, notification } from 'antd';
import Axios from 'axios';
import { useEffect, useState } from 'react';
import { alertMessage, debounce } from './function';
notification.config({
    maxCount: 1,
    duration: 2,
});

Axios.interceptors.response.use(
    (response) => {

        if (response.status === 400) {
            notification.error({
                message: 'Notification!',
                description: response.data.title,
            });
        }
        if (response.status === 201) {
            notification.success({
                message: 'Notification!',
                description: "Successfully",
            });
        }
        console.log(response)

        // do something with the response data
        if (response && response.data.statusCode === STATUSCODE_500) {
            notification.error({
                message: 'Notification!',
                description: response.data.message,
            });
        }
        if (response && response.data.statusCode === STATUSCODE_200 && response.data.message) {
            notification.success({
                message: 'Notification!',
                description: response.data.message,
            });
        }
        return response;
    },
    (error) => {
        notification.config({
            maxCount: 1,
            duration: 2,
        });
        let mess = '';
        if (error.response.status === STATUSCODE_401) {
            window.location.href = '/login';
            localStorage.clear();
            return;
        }
        if (error && error.response) {
            mess = error.response.data.message;
            if (mess) {
                console.log('res', mess);
                notification.error({
                    message: 'Notification!',
                    description: mess,
                });
            }
        } else {
            notification.error({
                message: 'Notification!',
                description: 'Error',
                maxCount: 1,
            });
        }
        return error.response;
    },
);

async function defaultGet(endpoint) {
    return await Axios({
        method: METHOD_GET,
        url: endpoint,
    });
}
export async function getData({ url, onSuccess }) {
    // setLoading(true);
    try {
        const res = await defaultGet(url);
        if (res && res.data) {
            onSuccess(res.data);
        }
    } catch (err) {
    } finally {
        // setLoading(false);
    }
}

async function defaultPost(endpoint, method, payload) {
    const body = {};
    Object.keys(payload).forEach((key) => {
        body[key] = payload[key];

        if (payload[key] || typeof payload[key] === 'boolean' || typeof payload[key] === 'number') {
            body[key] = payload[key];
        }
        return null;
    });
    return await Axios({
        headers: {},
        method: method,
        url: endpoint,
        data: body,
    });
}
export async function authGetData({ url, onSuccess, setLoading }) {
    setLoading(true);
    try {
        const res = await authGet(url);
        if (res && res.data) {
            onSuccess(res.data);
        }
    } catch (err) {
    } finally {
        setLoading(false);
    }
}

export async function postData({ url, payload, method = METHOD_POST, onSuccess, setLoading }) {
    setLoading(true);
    try {
        const res = await defaultPost(url, method, payload);
        if (res && res.data) {
            onSuccess({ res, statusCode: 200 });
        }
    } catch (err) {
        console.log('err' + err);
    } finally {
        setLoading(false);
    }
}

export async function authGet(endpoint) {
    const token = localStorage.getItem(TOKEN_NAME);
    return await Axios({
        headers: {
            Authorization: `Bearer ${token}`,
        },
        method: METHOD_GET,
        url: endpoint,
    });
}

async function authPost(endpoint, method, payload) {
    const token = localStorage.getItem(TOKEN_NAME);
    const body = {};
    Object.keys(payload).forEach((key) => {
        if (payload[key] || typeof payload[key] === 'boolean' || typeof payload[key] === 'number') {
            body[key] = payload[key];
        }
        return {};
    });
    return await Axios({
        headers: {
            Authorization: `Bearer ${token}`,
        },
        method: method,
        url: endpoint,
        data: body,
    });
}
export async function authPostData({ url, method, payload, setLoading, onSuccess }) {
    setLoading(true);
    try {
        const res = await authPost(url, method, payload);

        if (res && res.data) {
            console.log('res: ', res)
            onSuccess({ statusCode: 200 });
        }
    } catch (err) {
    } finally {
        setLoading(false);
    }
}


/// delete
async function authDelete(endpoint) {
    const token = localStorage.getItem(TOKEN_NAME);
    return await Axios({
        headers: {
            Authorization: `Bearer ${token}`,
        },
        method: METHOD_DELETE,
        url: endpoint,
    });
}
export async function startDelete({ url, setLoading, onSuccess }) {
    setLoading(true);
    try {
        const res = await authDelete(url);
        console.log('delete ', res)
        if (res) {
            onSuccess({ statusCode: 200 });
        }
    } catch (err) {
    } finally {
        setLoading(false);
    }
}
export function authDeleteData({
    url,
    setLoading,
    onSuccess,
    content = 'Are you sure you want to delete?',
    title = 'Confirmation',
}) {
    Modal.confirm({
        centered: true,
        title,
        content,
        onOk() {
            startDelete({ url, setLoading, onSuccess });
        },
        onCancel() { },
        okText: 'OK',
        okButtonProps: { type: 'danger' },
        cancelText: 'Cancel',
    });
}

function getFileName(response) {
    let filename = "";
    const disposition = response.headers["content-disposition"];
    if (disposition && disposition.indexOf("filename") !== -1) {
        const filenameRegex = /UTF-8(.*)/;
        const matches = filenameRegex.exec(disposition);
        if (matches != null && matches[1]) {
            filename = decodeURIComponent(matches[1].replace(/['"]/g, ""));
        }
    }
    return filename;
}

export async function downloadFile({ endpoint, setLoading }) {
    setLoading(true);
    const token = localStorage.getItem(TOKEN_NAME);
    try {
        const res = await Axios({
            headers: {
                Accept: "application/json",
                Authorization: `Bearer ${token}`,
            },
            responseType: "blob",
            method: "GET",
            url: endpoint,
        });

        const fileName = getFileName(res);
        if (res && res.data && res.status === 200) {
            const url = window.URL.createObjectURL(new Blob([res.data]));
            const link = document.createElement("a");
            link.href = url;
            link.setAttribute("download", fileName ? fileName : "template.xlsx");
            document.body.appendChild(link);
            link.click();
        }
        if (res && res.data && res.status === 422) {
            return notification.error({
                message: `Hãy nhập đủ điều kiện tìm kiếm`,
            });
        } else if (fileName === "") {
            alertMessage(
                "error",
                "Thông báo",
                res.message.message ? res.message.message : "Dữ liệu không tìm thấy"
            );
        }
    } catch (err) {
    } finally {
        setLoading(false);
    }
}