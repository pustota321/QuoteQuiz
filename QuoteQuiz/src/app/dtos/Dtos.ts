export interface PlayerDto{
    username : string;
    recordScore? : number;
  }

export  interface Question{
    quote : string;
    authors : AuthorDto[];
    }
  
export interface AuthorDto{
    id? : number;
    firstName : string;
    lastName : string;
    isAnswer? : boolean;
  }
  export interface QuoteDto{
    id? : number;
    content: string;
  }

  export interface CreateQuoteDto{
    content: string;
    authorFirstName : string;
    authorLastName : string,
  }