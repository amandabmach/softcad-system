export interface Log {
    
    id: number;
    executorEmail: string;
    operation: string;
    timestamp: string;
    affectedUser: string;
    result: string;
    message: string;
    previousState: string;
    currentState: string;

}