export interface ApiResponse<T> {
    result: T;
    warnings: any;
    errors: ServerError[];
}

export interface ServerError {
    fieldName: string | null;
    code: string;
    message: string;
}