export interface JournalRequest {
    course: number,
    group: string,
    date: Date,
    subject: SubjectModel
}

export interface SubjectModel {
    subjectName: string,
    timeStart: string
}