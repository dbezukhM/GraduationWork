import { Guid } from "guid-typescript";

export interface WorkingProgramReject {
    workingProgramId: Guid
    reason: string
}
