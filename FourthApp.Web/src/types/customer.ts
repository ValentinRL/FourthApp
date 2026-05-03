export type Customer = {
    customerId: string;
    companyName: string;
    ordersCount: number;
};

export type Order = {
    orderId: number;
    totalAmount: number;
    totalItems: number;
};

export type GetCustomersOverviewResult = {
    customers: Customer[];
    page: number;
    pageSize: number;
    totalCount: number;
};

export type CustomerDetails = {
    customerId: string;
    companyName: string;
    contactName: string | null;
    contactTitle: string | null;
    address: string | null;
    city: string | null;
    postalCode: string | null;
    country: string | null;
    phone: string | null;
    orders: Order[];
};

export type GetCustomersRequest = {
    page: number;
    pageSize: number;
    search?: string;
};
