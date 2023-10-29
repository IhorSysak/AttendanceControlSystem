export interface JournalResponse {
    course: number,
    group: string,
    subjectName: string,
    studentPresenceInfos: StudentPresenceInfo[]
}

export interface StudentPresenceInfo {
    lastName: string,
    firstName: string,
    middleName: string,
    isPresent: boolean
}