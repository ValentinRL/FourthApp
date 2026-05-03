import type {
    CustomerDetails,
    GetCustomersOverviewResult,
    GetCustomersRequest,
} from "../types/customer";

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL;

async function request<T>(url: string): Promise<T> {
    const response = await fetch(url);

    if (!response.ok) {
        throw new Error(`Request failed with status ${response.status}`);
    }

    return response.json() as Promise<T>;
}

export function getCustomers(
    params: GetCustomersRequest
): Promise<GetCustomersOverviewResult> {
    const query = new URLSearchParams({
        page: params.page.toString(),
        pageSize: params.pageSize.toString(),
    });

    if (params.search?.trim()) {
        query.set("search", params.search.trim());
    }

    return request<GetCustomersOverviewResult>(
        `${API_BASE_URL}/customers?${query.toString()}`
    );
}

export function getCustomerById(customerId: string): Promise<CustomerDetails> {
    return request<CustomerDetails>(
        `${API_BASE_URL}/customers/${encodeURIComponent(customerId)}`
    );
}