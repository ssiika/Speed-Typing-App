import { Component, HostListener, ViewChild, ElementRef } from '@angular/core';
import { GameService } from '../../services/gameService/game.service';

@Component({
  selector: 'app-game',
  templateUrl: './game.component.html',
  styleUrls: ['./game.component.css']
})
export class GameComponent {
  constructor(
    private gameService: GameService
  ) { }

  isLoading: boolean = true;
  isStarted: boolean = false;
  isCompleted: boolean = false;
  text: string = '';
  time: number = 0.0;
  mistakes: number = 0;
  pointer: number = 0;

  @HostListener('document:keypress', ['$event'])
  handleKeyboardEvent(event: KeyboardEvent) {
    this.handleKey(event.key);
  }

  handleKey(key: string) {
    if (this.pointer === 0) {
      this.isStarted = true;
    }

    if (key === this.text.charAt(this.pointer)) {
      // Key correct. Advance pointer
      document.getElementById(`${this.pointer}`)?.classList.remove('wrong');
      document.getElementById(`${this.pointer}`)?.classList.remove('highlighted');
      document.getElementById(`${this.pointer}`)?.classList.add('correct');
      
    } else {
      // Key wrong
      document.getElementById(`${this.pointer}`)?.classList.add('wrong');
      this.mistakes++
      return;
    }

    this.pointer++
    document.getElementById(`${this.pointer}`)?.classList.add('highlighted');

    // Check if text is complete

    if (this.pointer === this.text.length) {
      console.log('You finished!')
    }
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

  ngOnInit(): void {

    // This timeout is only here to show off the loading screen, it does not serve any important function

    setTimeout(() => {
      this.getText();
    }, 0)    
  }
}
