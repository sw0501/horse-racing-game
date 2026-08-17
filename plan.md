# 경마 게임 — 진행 상황

마지막 커밋: `8635549` "Feat: 빌드 파일 추가" (2024-04-29). 이후 2026-08-15~17에 아래 작업을 진행:
1. 신규 기능 7종(시작 화면, 참가 인원 조절, 동물 선택, 신호등, y축 이동, 결과 화면, 재시작) 스크립트 레이어 구현
2. 코드/에셋 개선사항 4종 반영(중복 에셋 정리, 배경 패럴랙스, 매직 넘버 노출, Find 캐싱)
3. 재미 요소 5종(동물별 능력치, 실시간 순위 HUD, 포토피니시, 부스트/장애물 존, 로컬 기록 저장) 구현 + 그중 3종은 플레이 전 온오프 토글 지원

**현재 상태 요약**: C# 스크립트 레이어는 전부 구현·컴파일 확인 완료. 이 환경은 Unity 에디터 GUI를 조작할 수 없어 씬 UI 생성·Inspector 연결은 전혀 못했음 — 즉 **에디터 연결 작업(아래 체크리스트 12개 항목)이 하나도 안 된 상태**라 지금 Play를 눌러도 게임이 정상 동작하지 않음. 남은 작업은 전부 GitHub 이슈로 등록함 ([이슈 목록](https://github.com/sw0501/horse-racing-game/issues)).

### 등록된 이슈

**필수 배선** (이게 끝나야 Play로 게임이 돌아감)
- [#1 GameManager 오브젝트 생성 및 GameFlowController 필드 연결](https://github.com/sw0501/horse-racing-game/issues/1)
- [#2 EndMenu 구성 (결과 화면 + 처음으로 버튼)](https://github.com/sw0501/horse-racing-game/issues/2)
- [#3 NextButton에 OnHorseCountConfirmed 연결](https://github.com/sw0501/horse-racing-game/issues/3)

**기능별 에디터 배치** (선택 기능)
- [#4 출발 신호등 UI 제작](https://github.com/sw0501/horse-racing-game/issues/4)
- [#5 ChooseName 동물 선택 화면 구성 + Fox/Rabbit Animator Controller 제작](https://github.com/sw0501/horse-racing-game/issues/5)
- [#6 말 오브젝트에 AnimalVisual 부착 및 Controller 연결](https://github.com/sw0501/horse-racing-game/issues/6)
- [#7 배경 패럴랙스 레이어 배치](https://github.com/sw0501/horse-racing-game/issues/7)
- [#8 실시간 순위 HUD 배치](https://github.com/sw0501/horse-racing-game/issues/8)
- [#9 포토피니시 연출 오브젝트 배치](https://github.com/sw0501/horse-racing-game/issues/9)
- [#10 부스트/장애물 존 배치](https://github.com/sw0501/horse-racing-game/issues/10)
- [#11 게임 옵션 온오프 토글 UI 배치](https://github.com/sw0501/horse-racing-game/issues/11)

**QA**
- [#12 전체 플로우 동작 확인](https://github.com/sw0501/horse-racing-game/issues/12) (#1~#11 이후 진행)

**신규 기능**
- [#13 베팅(예측) 시스템 추가](https://github.com/sw0501/horse-racing-game/issues/13)
- [#14 효과음 강화](https://github.com/sw0501/horse-racing-game/issues/14)
- [#15 날씨/트랙 컨디션 랜덤 이벤트](https://github.com/sw0501/horse-racing-game/issues/15)

**정리**
- [#16 스텁 스크립트(HorseRemoveLine, test) 정리](https://github.com/sw0501/horse-racing-game/issues/16)
- [#17 레포/빌드 관리 프로세스 정리](https://github.com/sw0501/horse-racing-game/issues/17)

## 완료된 기능 (기존)

README에 명시된 3가지 핵심 기능:

- [x] **말의 개별 속도 랜덤 지정** — `Horse_Move.cs`: 매 `FixedUpdate`마다 `Random.Range(0.02f, 0.2f)`로 이동 속도를 새로 굴려 각 말이 독립적으로 전진.
- [x] **카메라 출발 및 멈춤 기능** — `CamerMoveStart.cs` / `CameraMoveEnd.cs` 트리거가 `CameraMove.cs`의 활성/비활성을 토글.
- [x] **도착 순위 판별 기능** — `HorseOff.cs`: 결승선 트리거에서 태그(`"1"`~`"5"`)로 말을 구분해 도착 순서 카운터를 `TMP_Text`에 기록.

추가로 커밋 로그 기준 구현된 것:

- [x] **낙마(fall) 기능** — `Horse_Move.cs`의 `LiveOrDie` 로직 + `HosreDieOn.cs`.
- [x] **출발 대기 연출** — `wait.cs`(현재는 아래 로드맵 반영으로 자동 실행에서 수동 트리거 방식으로 변경됨).
- [x] **빌드 산출물** — `build.zip`이 저장소에 커밋되어 있음.
- [x] **한글 주석 인코딩 복구** — `Horse_Move.cs`가 CP949로 저장돼 있던 것을 UTF-8로 재저장.

## 미완성 / 스텁 상태 코드

- [x] `Assets/script/Backgorund_Move.cs` — 패럴랙스 스크롤 스크립트로 구현 완료(아래 "개선하면 좋을 사항" 참고). 씬의 배경 오브젝트에 붙이는 건 에디터 작업 필요.
- [ ] `Assets/HorseRemoveLine.cs` — 이름상 완주 후 말 제거/정리 기능으로 추정되나 로직 없음.
- [ ] `Assets/script/test.cs` — 테스트용 빈 스크립트. 정리 또는 삭제 검토.

## 로드맵 — 신규 기능 7종 구현 현황

**스크립트(C#) 레이어는 전부 구현 완료.** 단, 이 환경은 Unity 에디터 GUI를 조작할 수 없어 씬(.unity)의 새 UI 생성·Inspector 참조 연결·프리팹 작업은 하지 못함. 아래 "에디터 작업 체크리스트"를 사람이 직접 완료해야 실제로 동작함(그 전까지는 기능 미완료로 간주).

씬 조사 결과, `Canvas` 하위에 이미 만들어져 있었지만 비활성/미연결 상태였던 UI를 최대한 재사용함:
- `StartMenu`(비활성) — `SelectHorseNum` 드롭다운("2 Horse"~"5 Horse") + `NextButton`
- `ChooseName`(비활성, 자식 없는 빈 껍데기) — 동물 선택 화면 예정 위치
- `Ranking`(활성) — 순위 `TMP_Text` 5개, `HorseOff`가 이미 참조
- `EndMenu`(비활성, 배경 스프라이트만 존재, 버튼 없음)

### 1. 게임 시작 화면
- [x] 스크립트 구현 — `GameFlowController.cs`(`Awake()`에서 `StartMenu` 활성화, `ChooseName`/`EndMenu` 비활성화, `RaceConfig` 초기화) + `wait.cs`(자동 실행 제거, `BeginWait()` 공개 메서드로 전환).
- [ ] 에디터 연결 — 체크리스트 1, 4번 참고. **연결 전까지는 Play 해도 경주가 시작되지 않음.**

### 2. 참가 플레이어 수 조절 기능
- [x] 스크립트 구현 — `GameFlowController.OnHorseCountConfirmed()`, `CameraMove.cs`(하드코딩 5마리 제거, `RaceConfig.HorseCount` 기반 순회로 재작성), `HorseOff.cs`(완주 인원 도달 시 `OnRaceFinished()` 호출), `Select_Horse_Number.cs`(드롭다운 → `RaceConfig` 반영).
- [ ] 에디터 연결 — 체크리스트 1, 4번 참고.

### 3. 플레이어 동물 선택 기능
- [x] 스크립트 구현 — `RaceConfig.cs`(`AnimalType` enum), `AnimalVisual.cs`(Animator Controller 교체), `AnimalSelectController.cs`(버튼 핸들러).
- [x] **범위 축소 결정**: 레인별 개별 동물이 아니라 참가자 전원이 같은 동물 종으로 통일되도록 구현(레인별 선택은 UI 복잡도가 크게 늘어나 이번 범위에서 제외, 필요시 추후 확장).
- [ ] 에디터 연결 — 체크리스트 5, 6번 참고. `ChooseName` 화면 자체를 새로 만들어야 함.
- [ ] Fox/Rabbit용 Animator Controller 애셋 제작(현재 스프라이트만 있고 컨트롤러 없음).

### 4. 출발 직전 신호등 UI
- [x] 스크립트 구현 — `StartLightController.cs`(순차 점등 후 `wait.BeginWait(0f)` 호출, 자체 지연이 `wait.cs`의 기본 4.5초를 대체).
- [ ] 신호등 스프라이트/UI 제작 및 에디터 연결(선택 사항, 없으면 자동으로 기존 4.5초 무연출 대기로 대체됨) — 체크리스트 3번 참고.

### 5. 동물 움직임 y축 추가
- [x] 스크립트 구현 완료 — `Horse_Move.cs`: 시작 y좌표(`startY`) 기준 `verticalWobbleRange`(기본 0.3) 내에서 매 프레임 `verticalWobbleSpeed`(기본 0.05)만큼 랜덤 누적·클램프. x축 전진 로직은 유지. **에디터 작업 불필요, 바로 동작.**

### 6. 게임 종료 후 순위 보여주는 화면
- [x] 스크립트 구현 — `EndMenuController.cs`(`ShowResults()`), `HorseOff.cs` → `GameFlowController.OnRaceFinished()` → `EndMenuController.ShowResults()` 연쇄 호출.
- [ ] 에디터 작업 — `EndMenu`에 순위 텍스트 배치(현재 배경 스프라이트만 있음) 및 연결. 체크리스트 2번 참고.

### 7. 게임 시작 화면으로 돌아가는 기능
- [x] 스크립트 구현 완료 — `EndMenuController.ReturnToStart()`가 `SceneManager.LoadScene(현재 씬)`으로 전체 상태를 재로드 방식으로 초기화(필드 단위 수동 리셋보다 안전하다고 판단해 이 방식 채택). 씬이 하나뿐이라 별도 시작 화면 씬 불필요.
- [ ] 에디터 작업 — `EndMenu`에 "처음으로" 버튼 생성 및 연결. 체크리스트 2번 참고.

## 에디터 작업 체크리스트 (Unity에서 사람이 직접 해야 함)

**⚠️ 아래 작업 전부가 끝나기 전까지는 `wait.cs`가 더 이상 자동 실행되지 않으므로, 씬을 그냥 Play 하면 경주가 시작되지 않음.**

- [ ] **1. GameManager 오브젝트 생성**
  - 빈 GameObject(예: `GameManager`)를 씬 루트에 생성하고 `GameFlowController` 스크립트를 붙임.
  - Inspector에서 연결: `startMenu`=`StartMenu`, `chooseAnimalMenu`=`ChooseName`, `endMenu`=`EndMenu`, `horseNumberSelector`=`SelectHorseNum`(Select_Horse_Number 컴포넌트), `waitScript`=기존 `wait` 컴포넌트가 붙은 오브젝트, `startLight`=3번에서 만들 신호등 컨트롤러(선택), `endMenuController`=2번에서 만들 컴포넌트.

- [ ] **2. EndMenu 구성**
  - `EndMenu` 오브젝트(또는 그 자식)에 `EndMenuController` 스크립트를 붙이고 `endMenu` 필드에 `EndMenu` 자기 자신을 연결.
  - `EndMenu` 안에 "처음으로" 버튼을 새로 만들고 OnClick에 `EndMenuController.ReturnToStart` 연결.
  - 최종 순위를 보여줄 텍스트(예: `Ranking`의 5개 TMP_Text를 그대로 자식으로 옮기거나 복제)를 배치.
  - `HorseOff` 컴포넌트의 새 `gameFlow` 필드에 1번에서 만든 `GameFlowController`를 연결.
  - (선택) TMP_Text를 하나 더 추가해 `EndMenuController.recordSummaryText`에 연결하면 "누적 N판 플레이 / 최고 기록 M초" 요약이 표시됨(로컬 기록 저장 기능용).

- [ ] **3. 신호등 UI (선택 사항, 없으면 4.5초 무연출 대기로 대체됨)**
  - 신호등 역할을 할 GameObject/스프라이트 2~3개(빨강/노랑/초록 등)를 만들고 빈 GameObject에 `StartLightController`를 붙여 `lights` 배열에 순서대로 연결.
  - `waitScript` 필드에 기존 `wait` 컴포넌트를 연결.
  - `GameManager`의 `GameFlowController.startLight` 필드에 이 컴포넌트를 연결(연결하지 않으면 자동으로 신호등 없이 기존 4.5초 대기로 진행됨 — 정상 동작).

- [ ] **4. NextButton 추가 연결**
  - `NextButton`의 OnClick 리스트에 기존 `StartMenu.SetActive(false)` 호출은 그대로 두고, 새 호출을 추가: `GameFlowController.OnHorseCountConfirmed`.

- [ ] **5. ChooseName(동물 선택) 화면 구성**
  - `ChooseName`은 현재 완전히 빈 오브젝트임. 동물 수만큼(Horse/Bear/Boar/Deer/Fox/Wolf/Rabbit) 버튼을 만들고, `AnimalSelectController` 스크립트를 붙인 오브젝트를 하나 만들어 `flow` 필드에 `GameFlowController`를 연결.
  - 각 버튼의 OnClick에 `AnimalSelectController.SelectAnimal`을 연결하고 정수 인자를 `AnimalType` 순서(0=Horse, 1=Bear, 2=Boar, 3=Deer, 4=Fox, 5=Wolf, 6=Rabbit)에 맞게 지정.
  - `ChooseName` 화면을 만들지 않고 건너뛰려면 `GameFlowController.chooseAnimalMenu` 필드를 비워두면 됨(자동으로 동물 선택 없이 바로 경주 시작).

- [ ] **6. 말 오브젝트에 AnimalVisual 부착**
  - `horse_1`~`horse_5` 각각에 `AnimalVisual` 컴포넌트를 붙이고 `controllers` 배열에 `AnimalType`별 `RuntimeAnimatorController`를 지정.
    - Horse → 기존에 쓰던 말 컨트롤러(각 horse_N에 이미 할당된 것과 동일한 애셋).
    - Bear → `Assets/Wild Animals/Bear/Bear_Run.controller`
    - Boar → `Assets/Wild Animals/Boar/Square.controller`(이름이 어색하지만 유일하게 존재하는 Boar용 컨트롤러)
    - Deer → `Assets/Wild Animals/Deer/Deer.controller`
    - Wolf → `Assets/Wild Animals/Wolf/Wolf.controller`
    - Fox, Rabbit → **컨트롤러 애셋이 아직 없음.** 확인해보니 `Fox_Run.png` 등은 `spriteMode: 1`(Single) 즉 프레임이 여러 장으로 슬라이스된 스프라이트 시트가 아니라 정지 이미지 1장뿐임 — 애니메이션 클립을 만들려면 프레임이 나뉜 원본 아트가 추가로 필요하고, 이는 코드로 대체할 수 없는 아트 작업임. 급하면 `AnimalSelectController` 버튼에서 Fox/Rabbit 항목을 빼고 5종만 제공하거나, 정지 이미지 그대로 쓰는 Idle 전용 Animator Controller(단일 상태, 애니메이션 없이 스프라이트만 표시)로 대체.

- [ ] **7. 동작 확인**
  - Play 진입 → `StartMenu` 표시 확인 → 마릿수 선택 후 Next → (동물 선택 화면 구성했다면) 동물 선택 → 신호등(구성했다면) → 경주 자동 시작 → 결승 후 `EndMenu` 노출 → "처음으로" 버튼으로 `StartMenu` 복귀까지 전체 플로우 1회 통관.

- [ ] **8. 배경 패럴랙스(선택 사항)**
  - 배경 레이어로 쓸 오브젝트(예: 기존 `Background` 타일과 별도로 더 뒤에 배치할 원경 레이어)에 `Backgorund_Move` 컴포넌트를 붙이고 `cameraTransform`에 메인 카메라를 연결(비워두면 `Camera.main`을 자동 사용).
  - `parallaxFactor`(0~1)로 카메라 대비 이동 비율 조절 — 낮을수록 더 멀리 있는 느낌.

- [ ] **9. 실시간 순위 HUD 배치**
  - `Canvas` 하위에 TMP_Text 오브젝트를 하나 새로 만들어(예: 화면 좌상단) `LiveRankingController` 컴포넌트를 붙인 오브젝트(별도 빈 GameObject 또는 그 텍스트 오브젝트 자신)에서 `liveRankingText` 필드에 연결.
  - `GameManager`의 `GameFlowController.liveRanking` 필드에 이 컴포넌트를 연결(비워두면 자동 정지 기능만 생략되고 HUD 자체는 계속 동작 — 필수 아님).

- [ ] **10. 포토피니시 연출 배치**
  - 빈 GameObject(예: `PhotoFinish`)를 만들어 `PhotoFinishController`를 붙임. `raceCamera` 필드는 비워두면 `Camera.main` 자동 사용.
  - `HorseOff` 컴포넌트의 `photoFinish` 필드에 연결(비워두면 연출 없이 기존 순위 기록 동작만 유지).
  - 필요시 `slowMotionScale`(기본 0.2), `slowMotionDurationSeconds`(기본 1.5초), `zoomOrthographicSize`(기본 3), `photoFinishThresholdSeconds`(HorseOff 쪽, 기본 0.3초)를 플레이테스트하며 조정.

- [ ] **11. 부스트/장애물 존 배치**
  - 트랙 위 원하는 위치에 트리거 콜라이더(2D)가 달린 오브젝트를 만들고 `BoostZone` 컴포넌트를 붙임.
  - `speedMultiplier`(1보다 크면 부스트, 작으면 감속/장애물)와 `durationSeconds`를 구간별로 다르게 설정해 여러 개 배치 가능(`HosreDieOn`처럼 트랙 중간 아무 곳에나 놓을 수 있음).

- [ ] **12. 게임 시작 전 기능 온오프 토글 배치**
  - `StartMenu` 안에 Toggle UI 3개(예: "동물별 능력치", "부스트/장애물 존", "기록 저장")를 새로 만듦.
  - `GameManager`의 `GameFlowController`에서 `useAnimalStatsToggle`/`useBoostZonesToggle`/`saveLocalRecordsToggle` 필드에 각각 연결. 3개 모두 값은 `NextButton` 클릭 시점(`OnHorseCountConfirmed`)에 읽으므로 Toggle의 `OnValueChanged` 이벤트는 따로 연결할 필요 없음(마릿수 드롭다운과 동일한 패턴).
  - 연결하지 않고 비워두면 해당 기능은 항상 켜진 것으로 동작(하위 호환).

## 개선하면 좋을 사항

- [x] **중복 에셋 정리** — GUID로 실제 씬 참조 여부를 확인해 미사용 사본 삭제: `Assets/Scenes/Mario Kart Race Start Sound Effect.mp3`(씬은 `Assets/`의 것만 참조), `Assets/script/하츠네미쿠_-_폿핏포_.mp3`(내용도 다른 별도 편집본, 씬은 `Assets/`의 `(mp3cut.net)` 버전만 참조), 루트 `map.jpg`(Assets 밖이라 Unity 애셋도 아니고 README도 미참조) 삭제.
- [x] **`Backgorund_Move.cs` 구현** — 카메라 이동량의 일부(`parallaxFactor`, 기본 0.5)만큼만 따라 움직이는 범용 패럴랙스 스크립트로 구현. 기존 `Background` 타일 18개의 배치는 건드리지 않았고(위치를 검증 없이 옮기면 트랙과 어긋날 위험이 있어 그대로 둠), 별도 배경 레이어에 붙이는 용도로 사용 가능. **에디터 작업 필요**: 붙일 대상 오브젝트에 컴포넌트 추가 + `parallaxFactor` 튜닝.
- [x] **매직 넘버 Inspector 노출** — `Horse_Move.cs`에 `fallCheckRange`(기본 50000)/`fallCheckTarget`(기본 44)/`minSpeed`/`maxSpeed` 필드 추가, `wait.cs`의 `startDelaySeconds`는 앞서 기능 구현 때 이미 필드로 노출됨.
- [x] **`GameObject.Find` 매 프레임 호출 캐싱** — `CameraMove.cs`가 `Start()`에서 `horse_1`~`horse_5` 참조를 1회만 `Find`로 캐싱하고, 이후 `FixedUpdate`는 캐시된 배열을 `RaceConfig.HorseCount`만큼만 순회하도록 변경.
- [x] **Fox/Rabbit Animator Controller 관련 확인** — 원인 파악 완료: `Fox_Run.png` 등이 `spriteMode: 1`(Single, 슬라이스 안 된 정지 이미지 1장)이라 애니메이션 클립 자체를 만들 수 없는 상태(코드로 대체 불가한 아트 리소스 부족). 체크리스트 6번에 대안(5종만 제공 또는 정지 이미지용 단일 상태 컨트롤러) 추가.

## 넣으면 재밌을 기능

동물별 능력치/실시간 HUD/포토피니시/부스트존/로컬 기록은 구현 완료. 남은 후보 중 **베팅 시스템**이 이미 구현된 동물별 능력치 차별화와 바로 시너지가 나서(내가 고른 동물의 스탯을 보고 우승마를 예측) 우선순위로 추천.

- [ ] **베팅(예측) 시스템** — 레이스 시작 전 "이번 판 우승마"를 하나 찍게 하고, 결과 화면에서 적중 여부를 보여줌. 가상 포인트 + 배당률을 추가하면 더 재밌어짐.
- [x] **동물별 능력치 차별화** — 신규 `AnimalStats.cs`: 종별로 `minSpeed`/`maxSpeed`/낙마 확률 배율(`fallRiskMultiplier`) 프로필을 정의(예: 곰=느리지만 낙마 확률 0.5배, 토끼=빠르지만 1.6배). `GameFlowController.ApplySelectedAnimal()`이 동물 선택 확정 시 해당 프로필을 `Horse_Move`에 적용. `RaceConfig.UseAnimalStats` 토글로 온오프 가능(꺼져 있으면 기존처럼 전종 동일 스탯).
- [x] **실시간 순위 HUD** — 신규 `LiveRankingController.cs`: `horse_1`~`horse_5` 참조를 `CameraMove`와 동일한 방식으로 1회 캐싱한 뒤 `updateIntervalSeconds`(기본 0.2초)마다 x좌표 기준으로 정렬해 TMP_Text 1개에 "1위: N번" 줄바꿈 목록으로 갱신. `GameFlowController.OnRaceFinished()`에서 `liveRanking.enabled = false`로 자동 정지(필드 비워두면 무시). **에디터 작업 필요**: 목록을 표시할 TMP_Text 오브젝트 1개 신규 배치 + 연결.
- [x] **접전 연출(포토피니시)** — 신규 `PhotoFinishController.cs`(카메라 `orthographicSize`를 줌인 → `Time.timeScale`을 낮춰 슬로우모션(실시간 `WaitForSecondsRealtime`으로 지속시간 보장) → 줌아웃 순서로 재생). `HorseOff.cs`가 1위 완주 시각을 기록해두고, 2위가 `photoFinishThresholdSeconds`(기본 0.3초) 이내에 들어오면 자동 트리거. **에디터 작업 필요**: 빈 오브젝트에 컴포넌트 부착 + `HorseOff.photoFinish` 필드 연결(비워두면 연출 없이 기존 동작 유지).
- [x] **부스트/장애물 존** — `Horse_Move.cs`에 `ApplySpeedMultiplier(multiplier, duration)` 추가(1보다 크면 부스트, 작으면 감속, 시간 지나면 자동 원복). 신규 `BoostZone.cs`가 `HosreDieOn`과 동일한 트리거 패턴으로 진입한 말에게 배율 적용. `RaceConfig.UseBoostZones` 토글로 온오프 가능(꺼져 있으면 존에 들어와도 아무 효과 없음). **에디터 작업 필요**: 트랙 위에 트리거 콜라이더 오브젝트를 만들어 `BoostZone` 부착 + `speedMultiplier`/`durationSeconds` 튜닝(여러 구간에 배치 가능).
- [ ] **효과음 강화** — 낙마 효과음, 결승선 통과 순위별 팡파레, 신호등 전환음(`StartLightController`에 붙이기 좋음) 추가.
- [x] **로컬 기록 저장** — 신규 `LocalRecordManager.cs`(`PlayerPrefs` 기반): 레인 번호별 누적 우승 횟수, 총 플레이 판 수, 최고 완주 기록(초)을 저장. `wait.cs`가 말이 실제로 출발하는 시점을 `RaceConfig.RaceStartTime`에 기록해두고, `HorseOff.cs`가 1위 완주 시 해당 레인·소요시간을 저장. `EndMenuController.recordSummaryText`(선택 필드)에 연결하면 결과 화면에 "누적 N판 플레이 / 최고 기록 M초" 요약 표시. `RaceConfig.SaveLocalRecords` 토글로 온오프 가능.
- [ ] **날씨/트랙 컨디션 랜덤 이벤트** — 판마다 "비 오는 날(속도 -20%)", "맑은 날(낙마 확률 감소)" 등을 시작 화면에 랜덤 노출해 매판 다른 느낌 부여.

## 알려진 이슈 / 정리 필요 사항

- [ ] 자동화된 빌드/테스트/CI 파이프라인 없음 — Unity Editor(2021.3.22f1)에서 수동 실행/빌드만 가능.
- [ ] `build.zip`처럼 바이너리 빌드 산출물을 저장소에 직접 커밋하는 방식 유지 여부 검토(용량 이슈).
- [ ] 스텁 스크립트(`HorseRemoveLine`, `test`) 실제 구현 여부 결정 필요.
- [ ] Fox/Rabbit용 Animator Controller가 없어 동물 선택 목록에 아직 넣을 수 없음(원본 스프라이트가 슬라이스 안 된 정지 이미지 1장뿐이라 아트 리소스 추가 필요).
