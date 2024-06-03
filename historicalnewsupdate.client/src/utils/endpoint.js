import { BASE_API_URL } from "./constants";

export const Endpoint = {
    LOGIN: `${BASE_API_URL}/auth/login`,
    CRUD_MARKET: `${BASE_API_URL}/stockexchanges`,
    CRUD_COMPANY: `${BASE_API_URL}/companies`,
    GET_ALL_COMPANIES: `${BASE_API_URL}/companies/list`,
    GET_ALL_MARKETS: `${BASE_API_URL}/stockexchanges/list`,
    CRUD_MARKET: `${BASE_API_URL}/stockexchanges`,
    
    CRUD_SUPPORT: `${BASE_API_URL}/supports`,
    GET_ALL_SUPPORT: `${BASE_API_URL}/supports/list`,
};
