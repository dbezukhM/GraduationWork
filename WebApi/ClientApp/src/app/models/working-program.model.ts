import { Guid } from "guid-typescript";

export interface WorkingProgram {
    id: Guid
    name: string
    subjectName: string
    isAvailable: boolean
}
