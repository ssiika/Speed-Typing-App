import { Component, HostListener, ViewChild, ElementRef } from '@angular/core';
import { Router } from "@angular/router";
import { GameService } from '../../services/gameService/game.service';
import { RecordService } from '../../services/recordService/record.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent {
  constructor(
    private router: Router,
    private recordService: RecordService,
    private gameService: GameService
  ) { }

  isLoading: boolean = true;
  isStarted: boolean = false;
  isCompleted: boolean = false;
  text: string = '';
  time: number = 0.0;
  pointer: number = 0;
  score: number = 0;

  // Timeout interval needs to be initialised so it can be called in stopTimer() later
  interval: number = 0;

  @HostListener('document:keypress', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    if (!this.isLoading && !this.isCompleted) {
      this.handleKey(event.key);
    }  
  }

  handleKey(key: string) {
    if (!this.isStarted) {
      this.isStarted = true;
      this.startTimer()
    }

    if (key === this.text.charAt(this.pointer)) {
      // Key correct. Advance pointer
      document.getElementById(`${this.pointer}`)?.classList.remove('wrong');
      document.getElementById(`${this.pointer}`)?.classList.remove('highlighted');
      document.getElementById(`${this.pointer}`)?.classList.add('correct');

    } else {
      // Key wrong
      document.getElementById(`${this.pointer}`)?.classList.add('wrong');
      return;
    }

    this.pointer++
    document.getElementById(`${this.pointer}`)?.classList.add('highlighted');

    // Check if text is complete

    if (this.pointer === this.text.length) {
      this.endGame()
    }
  }

  startTimer(): void {
    this.interval = window.setInterval(() => {
      this.time = Math.round((this.time + 0.1) * 10) / 10
    }, 100)
  }

  stopTimer(): void {
    clearInterval(this.interval)
  }

  endGame(): void {
    this.isStarted = false;
    this.isCompleted = true;
    this.stopTimer();
    this.score = Math.round((this.text.length / this.time) * 10 * 60) / 10;
    this.recordService.addRecord(this.score)
      .subscribe(res => {
        if (!res.success) {
          console.log(res.message)
        }
      }); 
  }

  getText(): void {
    this.gameService.getText()
      .subscribe(res => {
        if (res.success && res.data) {
          this.text = res.data.textString;
        } else {
          // Async GET did not work, retreive default text
          this.text = this.gameService.getDefaultText();
        }

        this.isLoading = false;
      });
  }

  manualRedirect() {

    // /records can instead be any valid placeholder route, the purpose of this function is to reload the current route

    this.router.navigateByUrl('/records', { skipLocationChange: true }).then(() => {
      this.router.navigate(['/'])
    });
  }

  ngOnInit(): void {

    // This timeout is only here to show off the loading screen, it does not serve any important function

    setTimeout(() => {
      this.getText();
    }, 1000)    
  }
}
