import { Component, OnInit } from '@angular/core';
import { Message } from '../_models/message';
import { Pagination } from '../_models/pagination';
import { ConfirmService } from '../_services/confirm.service';
import { MessageService } from '../_services/message.service';

@Component({
  selector: 'app-messeges',
  templateUrl: './messeges.component.html',
  styleUrls: ['./messeges.component.css']
})
export class MessagesComponent implements OnInit {

  messages: Message[];
  pagination: Pagination;
  container = 'Unread';
  pageNumber = 1;
  pageSiize = 5;
  loading = false;

  constructor(public messageService: MessageService, private confirmService: ConfirmService) { }

  ngOnInit(): void {
    this.loadMessages();
  }

  loadMessages(){
    this.loading = true;
    this.messageService.getMessages(this.pageNumber,this.pageSiize,this.container).subscribe(response=>{
      this.messages = response.result;
      this.pagination = response.pagination;
      this.loading = false;
    })

  }

  pageChanged(event: any){
    this.pageNumber = event.page;
    this.loadMessages();
  }

  deleteMessage(id:number){
    this.confirmService.confirm('Confirm delete message','This canot be undone').subscribe(result => {
      if(result){
        this.messageService.deleteMessage(id).subscribe(() => {
          this.messages.splice(this.messages.findIndex(m => m.id === id), 1);
        })
      }
    })
    
  }

}
