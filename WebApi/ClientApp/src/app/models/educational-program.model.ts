import { Guid } from "guid-typescript";

export interface EducationalProgram {
    id: Guid
    name: string
    universityName: string
    specializationName: string
    educationalProgramsTypeName: string
}
